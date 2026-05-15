using HospitalBilling.BLL;
using HospitalBilling.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HospitalBilling.UI
{
    public partial class frmBedStatus : System.Web.UI.Page
    {
        private readonly CabinManager _cabinManager = new CabinManager();
        private readonly BedManager _bedManager = new BedManager();
        private List<string> ControlsList
        {
            get
            {
                if (ViewState["controls"] == null)
                {
                    ViewState["controls"] = new List<string>();
                }
                return (List<string>)ViewState["controls"];
            }
        }

        private int NextId
        {
            get
            {
                return ControlsList.Count + 1;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            foreach (string txtId in ControlsList)
            {
                TextBox txt = new TextBox();
                txt.ID = "TextBox" + NextId.ToString();
                CabinPlaceHolder.Controls.Add(txt);
                CabinPlaceHolder.Controls.Add(new LiteralControl("<br>"));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                var cabin = _cabinManager.GetCabinByRoomTypeId();
                var bedList = _bedManager.GetBedByWardId();
                int i = 1;
                foreach (Cabin cb in cabin)
                {
                         TextBox txt = new TextBox();
                        txt.ID = "TextBox" + NextId.ToString();
                        txt.Text = cb.RoomTypeName + "\n " + cb.Name;

                        txt.Height = 50;
                        txt.Width = 60;
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.Font.Bold = true;
                        txt.Rows = 2;
                        txt.BorderStyle = BorderStyle.Dotted;
                        txt.ReadOnly = true;
                        txt.BackColor = Color.LightGray;
                        if (cb.Status == false)
                        {
                            txt.BackColor = Color.GhostWhite;
                        }
                        else
                        {
                            txt.BackColor = Color.Silver;
                        }
                        CabinPlaceHolder.Controls.Add(txt);
                        CabinPlaceHolder.Controls.Add(new LiteralControl("&nbsp"));
                        CabinPlaceHolder.Controls.Add(new LiteralControl("&nbsp"));
                    

                    i++;
                }


               
                foreach (Bed Wr in bedList)
                {

                    TextBox txt = new TextBox();
                    txt.ID = "TextBox" + i;
                    txt.Text = Wr.WardName + "\n " + Wr.Name;

                    txt.Height = 50;
                    txt.Width = 60;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.Font.Bold = true;
                    txt.Rows = 2;
                    txt.BorderStyle = BorderStyle.Dotted;
                    txt.ReadOnly = true;
                    txt.BackColor = Color.LightGray;
                    if (Wr.Status == false)
                    {
                        txt.BackColor = Color.GhostWhite;
                    }
                    else
                    {
                        txt.BackColor = Color.Silver;
                    }

                    WardPlaceHolder.Controls.Add(txt);
                    WardPlaceHolder.Controls.Add(new LiteralControl("&nbsp"));
                    WardPlaceHolder.Controls.Add(new LiteralControl("&nbsp"));

                    i++;
                }
            
           


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txt = new TextBox();
                txt.ID = "TextBox" + NextId.ToString();
                CabinPlaceHolder.Controls.Add(txt);
                CabinPlaceHolder.Controls.Add(new LiteralControl("<br>"));
                ControlsList.Add(txt.ID);          
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ale", "alert('Some Problem Hear!!');\n" + ex, true);
            }
        }
    }

}