﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRPApp.Classes;
using STG.SRP.ControlRooms;
using STG.SRP.Core.Utilities;
using STG.SRP.DAL;
using STG.SRP.Utilities.CoreClasses;

namespace STG.SRP.ControlRoom.Modules.Settings
{
    public partial class SchoolDistrictOriginal : BaseControlRoomPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage.IsSecure = true;
            MasterPage.PageTitle = string.Format("{0}", "School and District Crosswalk");

            if (!IsPostBack)
            {
                SetPageRibbon(StandardModuleRibbons.SettingsRibbon());

                LoadData();

            }
        }

        protected void LoadData()
        {
            var ds = SchoolCrosswalk.GetAll();
            rptrCW.DataSource = ds;
            rptrCW.DataBind();
        }

        protected void SaveData()
        {
            var rptr = rptrCW;
            int i = 0;
            bool errors = false;
            foreach (RepeaterItem item in rptr.Items)
            {

                i++;
                try
                {
                    var ID = int.Parse(((Label)item.FindControl("ID")).Text);
                    //var SchoolID = int.Parse(((DropDownList)item.FindControl("SchoolID")).SelectedValue);
                    var SchoolID = int.Parse(((TextBox)item.FindControl("SchoolID")).Text);
                    var SchTypeID = int.Parse(((DropDownList)item.FindControl("SchTypeID")).SelectedValue);
                    var DistrictID = int.Parse(((DropDownList)item.FindControl("DistrictID")).SelectedValue);
                    var City = ((TextBox)item.FindControl("City")).Text;

                    var MinGrade = ((TextBox)item.FindControl("MinGrade")).Text.SafeToInt();
                    var MaxGrade = ((TextBox)item.FindControl("MaxGrade")).Text.SafeToInt();
                    var MinAge = ((TextBox)item.FindControl("MinAge")).Text.SafeToInt();
                    var MaxAge = ((TextBox)item.FindControl("MaxAge")).Text.SafeToInt();


                    var obj = new SchoolCrosswalk();
                    if (ID != 0) obj.Fetch(ID);
                    obj.SchoolID = SchoolID;
                    obj.SchTypeID = SchTypeID;
                    obj.DistrictID = DistrictID;
                    obj.City = City;
                    obj.MinGrade = MinGrade;
                    obj.MaxGrade = MaxGrade;
                    obj.MinAge = MinAge;
                    obj.MaxAge = MaxAge;

                    if (ID != 0)
                    {
                        obj.Update();
                    }
                    else
                    {
                        obj.Insert();
                    }
                }
                catch (Exception ex)
                {
                    var masterPage = (IControlRoomMaster)Master;
                    masterPage.PageError = String.Format("On Row {1}: " + SRPResources.ApplicationError1, ex.Message, i);
                    errors = true;
                }

            }

            if (!errors)
            {
                var masterPage = (IControlRoomMaster)Master;
                masterPage.PageMessage = SRPResources.SaveAllOK;
            }
        }

        protected void btnBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/ControlRoom/Modules/Settings/Default.aspx");
        }

        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
            var masterPage = (IControlRoomMaster)Master;
            masterPage.PageMessage = SRPResources.RefreshAllOK;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveData();
            LoadData();
        }

        protected void btnSaveback_Click(object sender, ImageClickEventArgs e)
        {
            SaveData();
            Response.Redirect("~/ControlRoom/Modules/Settings/Default.aspx");
        }

        protected void rptrCW_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //var ctl = (DropDownList)e.Item.FindControl("SchoolID");
            //var txt = (TextBox)e.Item.FindControl("SchoolIDTxt");
            //var i = ctl.Items.FindByValue(txt.Text);
            //if (i != null) ctl.SelectedValue = txt.Text;

            var ctl = (DropDownList)e.Item.FindControl("SchTypeID");
            var txt = (TextBox)e.Item.FindControl("SchTypeIDTxt");
            var i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;

            ctl = (DropDownList)e.Item.FindControl("DistrictID");
            txt = (TextBox)e.Item.FindControl("DistrictIDTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;

        }
    }
}