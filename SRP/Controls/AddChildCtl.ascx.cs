﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using SRPApp.Classes;
using STG.SRP.ControlRoom;
using STG.SRP.Core.Utilities;
using STG.SRP.DAL;
using STG.SRP.Utilities.CoreClasses;

namespace STG.SRP.Controls
{
    public partial class AddChildCtl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["SA"]) && (Session["SA"] == null || Session["SA"].ToString() == ""))
                {
                    Session["SA"] = -1;
                }
                if (!string.IsNullOrEmpty(Request["SA"]))
                {
                    SA.Text = Request["SA"];
                    Session["SA"] = SA.Text;
                }
                else
                {
                    SA.Text = Session["SA"].ToString();
                }

                var patron = (Patron)Session["Patron"];
                var ds = Patron.GetPatronForEdit(patron.PID);
                ds.Tables[0].Rows[0]["Username"] = "";
                ds.Tables[0].Rows[0]["Password"] = "";
                ds.Tables[0].Rows[0]["Age"] = 0;
                ds.Tables[0].Rows[0]["DOB"] = DBNull.Value;
                ds.Tables[0].Rows[0]["SchoolGrade"] = "";
                ds.Tables[0].Rows[0]["ProgID"] = 0;
                ds.Tables[0].Rows[0]["FirstName"] = "";
                ds.Tables[0].Rows[0]["MiddleName"] = "";
                ds.Tables[0].Rows[0]["LastName"] = "";
                ds.Tables[0].Rows[0]["Gender"] = "";
                ds.Tables[0].Rows[0]["LiteracyLevel1"] = 0;
                ds.Tables[0].Rows[0]["LiteracyLevel2"] = 0;

                rptr.DataSource = ds;
                rptr.DataBind();



                ((BaseSRPPage)Page).TranslateStrings(rptr);

            }
        }

        protected void dv_DataBound(object sender, EventArgs e)
        {
        }

        protected void dv_DataBinding(object sender, EventArgs e)
        {
        }


        protected void rptr_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "cancel")
            {
                Response.Redirect("~/MyProgram.aspx");
            }

            lblError.Text = "";
            if (Page.IsValid)
            {
                if (e.CommandName == "save")
                {
                    if (SaveAccount())
                    {
                        Response.Redirect("~/FamilyAccountList.aspx");
                    }
                }
            }
        }

        protected void rptr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var ctl = (DropDownList)e.Item.FindControl("Gender");
            var txt = (TextBox)e.Item.FindControl("GenderTxt");
            var i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;


            ctl = (DropDownList)e.Item.FindControl("PrimaryLibrary");
            txt = (TextBox)e.Item.FindControl("PrimaryLibraryTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;


            ctl = (DropDownList)e.Item.FindControl("SchoolType");
            txt = (TextBox)e.Item.FindControl("SchoolTypeTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;

            //--
            ctl = (DropDownList)e.Item.FindControl("SchoolName");
            txt = (TextBox)e.Item.FindControl("SchoolNameTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;

            ctl = (DropDownList)e.Item.FindControl("SDistrict");
            txt = (TextBox)e.Item.FindControl("SDistrictTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;

            ctl = (DropDownList)e.Item.FindControl("District");
            txt = (TextBox)e.Item.FindControl("DistrictTxt");
            i = ctl.Items.FindByValue(txt.Text);
            if (i != null) ctl.SelectedValue = txt.Text;
            //--

            var cr = CustomRegistrationFields.FetchObject();
            if (cr.DDValues1 != "")
            {
                var ds = Codes.GetAlByTypeID(int.Parse(cr.DDValues1));
                ctl = (DropDownList)e.Item.FindControl("Custom1DD");
                txt = (TextBox)e.Item.FindControl("Custom1DDTXT");
                ctl.Items.Clear();
                ctl.Items.Add(new ListItem("[Select a Value]", ""));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ctl.Items.Add(new ListItem(ds.Tables[0].Rows[j]["Code"].ToString()));
                }

                i = ctl.Items.FindByValue(txt.Text);
                if (i != null) ctl.SelectedValue = txt.Text;
            }
            if (cr.DDValues2 != "")
            {
                var ds = Codes.GetAlByTypeID(int.Parse(cr.DDValues2));
                ctl = (DropDownList)e.Item.FindControl("Custom2DD");
                txt = (TextBox)e.Item.FindControl("Custom2DDTXT");
                ctl.Items.Clear();
                ctl.Items.Add(new ListItem("[Select a Value]", ""));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ctl.Items.Add(new ListItem(ds.Tables[0].Rows[j]["Code"].ToString()));
                }

                i = ctl.Items.FindByValue(txt.Text);
                if (i != null) ctl.SelectedValue = txt.Text;
            }
            if (cr.DDValues3 != "")
            {
                var ds = Codes.GetAlByTypeID(int.Parse(cr.DDValues3));
                ctl = (DropDownList)e.Item.FindControl("Custom3DD");
                txt = (TextBox)e.Item.FindControl("Custom3DDTXT");
                ctl.Items.Clear();
                ctl.Items.Add(new ListItem("[Select a Value]", ""));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ctl.Items.Add(new ListItem(ds.Tables[0].Rows[j]["Code"].ToString()));
                }

                i = ctl.Items.FindByValue(txt.Text);
                if (i != null) ctl.SelectedValue = txt.Text;
            }
            if (cr.DDValues4 != "")
            {
                var ds = Codes.GetAlByTypeID(int.Parse(cr.DDValues4));
                ctl = (DropDownList)e.Item.FindControl("Custom4DD");
                txt = (TextBox)e.Item.FindControl("Custom4DDTXT");
                ctl.Items.Clear();
                ctl.Items.Add(new ListItem("[Select a Value]", ""));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ctl.Items.Add(new ListItem(ds.Tables[0].Rows[j]["Code"].ToString()));
                }

                i = ctl.Items.FindByValue(txt.Text);
                if (i != null) ctl.SelectedValue = txt.Text;
            }
            if (cr.DDValues5 != "")
            {
                var ds = Codes.GetAlByTypeID(int.Parse(cr.DDValues5));
                ctl = (DropDownList)e.Item.FindControl("Custom5DD");
                txt = (TextBox)e.Item.FindControl("Custom5DDTXT");
                ctl.Items.Clear();
                ctl.Items.Add(new ListItem("[Select a Value]", ""));
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    ctl.Items.Add(new ListItem(ds.Tables[0].Rows[j]["Code"].ToString()));
                }

                i = ctl.Items.FindByValue(txt.Text);
                if (i != null) ctl.SelectedValue = txt.Text;
            }
        }


        public bool SaveAccount()
        {
            try
            {
                var patron = (Patron)Session["Patron"];
                var p = new Patron();
                DateTime _d;
                var DOB = rptr.Items[0].FindControl("DOB") as TextBox;
                if (DOB != null && DOB.Text != "")
                {
                    if (DateTime.TryParse(DOB.Text, out _d)) p.DOB = _d;
                }

                p.Age = FormatHelper.SafeToInt(((TextBox)(rptr.Items[0]).FindControl("Age")).Text);

                p.ProgID = FormatHelper.SafeToInt(((DropDownList)(rptr.Items[0]).FindControl("ProgID")).SelectedValue);
                p.Username = ((TextBox)(rptr.Items[0]).FindControl("Username")).Text;
                p.Password = ((TextBox)(rptr.Items[0]).FindControl("Password")).Text;

                p.IsMasterAccount = false;
                p.MasterAcctPID = patron.PID;

                p.SchoolGrade = ((TextBox)(rptr.Items[0]).FindControl("SchoolGrade")).Text;
                p.FirstName = ((TextBox)(rptr.Items[0]).FindControl("FirstName")).Text;
                p.MiddleName = ((TextBox)(rptr.Items[0]).FindControl("MiddleName")).Text;
                p.LastName = ((TextBox)(rptr.Items[0]).FindControl("LastName")).Text;
                p.Gender = ((DropDownList)(rptr.Items[0]).FindControl("Gender")).SelectedValue;
                p.EmailAddress = ((TextBox)(rptr.Items[0]).FindControl("EmailAddress")).Text;
                p.PhoneNumber = ((TextBox)(rptr.Items[0]).FindControl("PhoneNumber")).Text;
                p.PhoneNumber = FormatHelper.FormatPhoneNumber(p.PhoneNumber);
                p.StreetAddress1 = ((TextBox)(rptr.Items[0]).FindControl("StreetAddress1")).Text;
                p.StreetAddress2 = ((TextBox)(rptr.Items[0]).FindControl("StreetAddress2")).Text;
                p.City = ((TextBox)(rptr.Items[0]).FindControl("City")).Text;
                p.State = ((TextBox)(rptr.Items[0]).FindControl("State")).Text;
                p.ZipCode = ((TextBox)(rptr.Items[0]).FindControl("ZipCode")).Text;
                p.ZipCode = FormatHelper.FormatZipCode(p.ZipCode);

                p.Country = ((TextBox)(rptr.Items[0]).FindControl("Country")).Text;
                p.County = ((TextBox)(rptr.Items[0]).FindControl("County")).Text;
                p.ParentGuardianFirstName = ((TextBox)(rptr.Items[0]).FindControl("ParentGuardianFirstName")).Text;
                p.ParentGuardianLastName = ((TextBox)(rptr.Items[0]).FindControl("ParentGuardianLastName")).Text;
                p.ParentGuardianMiddleName = ((TextBox)(rptr.Items[0]).FindControl("ParentGuardianMiddleName")).Text;
                p.LibraryCard = ((TextBox)(rptr.Items[0]).FindControl("LibraryCard")).Text;

                //p.District = ((DropDownList)(rptr.Items[0]).FindControl("District")).SelectedValue;
                //p.SDistrict = ((DropDownList)(rptr.Items[0]).FindControl("SDistrict")).SelectedValue.SafeToInt();
                
                p.PrimaryLibrary = FormatHelper.SafeToInt(((DropDownList)(rptr.Items[0]).FindControl("PrimaryLibrary")).SelectedValue);
                p.SchoolName = ((DropDownList)(rptr.Items[0]).FindControl("SchoolName")).SelectedValue;
                p.SchoolType = FormatHelper.SafeToInt(((DropDownList)(rptr.Items[0]).FindControl("SchoolType")).SelectedValue);

                var lc = LibraryCrosswalk.FetchObjectByLibraryID(p.PrimaryLibrary);
                if (lc != null)
                {
                    p.District = lc.DistrictID == 0 ? ((DropDownList)(rptr.Items[0]).FindControl("District")).SelectedValue  : lc.DistrictID.ToString();
                }
                else
                {
                    p.District = ((DropDownList)(rptr.Items[0]).FindControl("District")).SelectedValue;
                }
                var sc = SchoolCrosswalk.FetchObjectBySchoolID(p.SchoolName.SafeToInt());
                if (sc != null)
                {
                    p.SDistrict = sc.DistrictID == 0 ? ((DropDownList)(rptr.Items[0]).FindControl("SDistrict")).SelectedValue.SafeToInt() : sc.DistrictID;
                    p.SchoolType = sc.SchTypeID == 0 ? FormatHelper.SafeToInt(((DropDownList)(rptr.Items[0]).FindControl("SchoolType")).SelectedValue) : sc.SchTypeID;
                }
                else
                {
                    p.SDistrict = ((DropDownList)(rptr.Items[0]).FindControl("SDistrict")).SelectedValue.SafeToInt();
                }

                p.Teacher = ((TextBox)(rptr.Items[0]).FindControl("Teacher")).Text;
                p.GroupTeamName = ((TextBox)(rptr.Items[0]).FindControl("GroupTeamName")).Text;
                p.LiteracyLevel1 = FormatHelper.SafeToInt(((TextBox)(rptr.Items[0]).FindControl("LiteracyLevel1")).Text);
                p.LiteracyLevel2 = FormatHelper.SafeToInt(((TextBox)(rptr.Items[0]).FindControl("LiteracyLevel2")).Text);

                p.ParentPermFlag = true;
                p.Over18Flag = true;
                p.ShareFlag = true;
                p.TermsOfUseflag = true;

                var cr = CustomRegistrationFields.FetchObject();
                p.Custom1 = cr.DDValues1 == "" ? ((TextBox)(rptr.Items[0]).FindControl("Custom1")).Text : ((DropDownList)(rptr.Items[0]).FindControl("Custom1DD")).SelectedValue;
                p.Custom2 = cr.DDValues2 == "" ? ((TextBox)(rptr.Items[0]).FindControl("Custom2")).Text : ((DropDownList)(rptr.Items[0]).FindControl("Custom2DD")).SelectedValue;
                p.Custom3 = cr.DDValues3 == "" ? ((TextBox)(rptr.Items[0]).FindControl("Custom3")).Text : ((DropDownList)(rptr.Items[0]).FindControl("Custom3DD")).SelectedValue;
                p.Custom4 = cr.DDValues4 == "" ? ((TextBox)(rptr.Items[0]).FindControl("Custom4")).Text : ((DropDownList)(rptr.Items[0]).FindControl("Custom4DD")).SelectedValue;
                p.Custom5 = cr.DDValues5 == "" ? ((TextBox)(rptr.Items[0]).FindControl("Custom5")).Text : ((DropDownList)(rptr.Items[0]).FindControl("Custom5DD")).SelectedValue;

                p.AvatarID = FormatHelper.SafeToInt(((System.Web.UI.HtmlControls.HtmlInputText)rptr.Items[0].FindControl("AvatarID")).Value);

                
                if (p.IsValid(BusinessRulesValidationMode.INSERT))
                {
                    p.Insert();

                    var prog = Programs.FetchObject(p.ProgID);
                    var list = new List<Badge>();
                    if (prog.RegistrationBadgeID != 0)
                    {
                        AwardPoints.AwardBadgeToPatron(prog.RegistrationBadgeID, p, ref list);                        
                    }
                    AwardPoints.AwardBadgeToPatronViaMatchingAwards(p, ref list);

                    patron.IsMasterAccount = true;
                    patron.Update();
                }
                else
                {
                    string message = String.Format(SRPResources.ApplicationError1, "<ul>");
                    foreach (BusinessRulesValidationMessage m in p.ErrorCodes)
                    {
                        message = string.Format(String.Format("{0}<li>{{0}}</li>", message), m.ErrorMessage);
                    }
                    message = string.Format("{0}</ul>", message);
                    lblError.Text = message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = String.Format(SRPResources.ApplicationError1, ex.Message);

                return false;
            }
            return true;
        }

        protected void City_TextChanged(object sender, EventArgs e)
        {
            ReloadLibraryDistrict();
            ReloadSchoolDistrict();
        }

        protected void District_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadLibraryDistrict();
        }

        protected void SchoolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadSchoolDistrict();
        }

        protected void SDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadSchoolDistrict();
        }

        protected void Age_TextChanged(object sender, EventArgs e)
        {
            ReloadSchoolDistrict();
        }

        protected void SchoolGrade_TextChanged(object sender, EventArgs e)
        {
            ReloadSchoolDistrict();
        }

        protected void ReloadSchoolDistrict()
        {
            //*
            var sc = (DropDownList)(rptr.Items[0]).FindControl("SchoolName");
            var st = (DropDownList)(rptr.Items[0]).FindControl("SchoolType");
            var sd = (DropDownList)(rptr.Items[0]).FindControl("SDistrict");
            var ag = (TextBox)(rptr.Items[0]).FindControl("Age");
            var gr = (TextBox)(rptr.Items[0]).FindControl("SchoolGrade");

            var scVal = sc.SelectedValue;
            sc.Items.Clear();
            sc.Items.Add(new ListItem("[Select a Value]", "0"));
            var ds = SchoolCrosswalk.GetFilteredSchoolDDValues(st.SelectedValue.SafeToInt(), sd.SelectedValue.SafeToInt(), city.Text, ag.Text.SafeToInt(), gr.Text.SafeToInt());
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                sc.Items.Add(new ListItem(r["Code"].ToString(), r["CID"].ToString()));
            }

            var si = sc.Items.FindByValue(scVal);
            sc.SelectedValue = si != null ? scVal : "0";
            //*            
        }

        protected void ReloadLibraryDistrict()
        {
            //*
            var pl = (DropDownList)(rptr.Items[0]).FindControl("PrimaryLibrary");
            var dt = (DropDownList)(rptr.Items[0]).FindControl("District");
            var plVal = pl.SelectedValue;
            pl.Items.Clear();
            pl.Items.Add(new ListItem("[Select a Value]", "0"));
            var ds = LibraryCrosswalk.GetFilteredBranchDDValues(int.Parse(dt.SelectedValue), city.Text);
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                pl.Items.Add(new ListItem(r["Code"].ToString(), r["CID"].ToString()));
            }
            var il = pl.Items.FindByValue(plVal);
            pl.SelectedValue = il != null ? plVal : "0";
            //*            
        }

    }
}