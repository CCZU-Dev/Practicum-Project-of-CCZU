﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BLL;

namespace Web
{
	public partial class T_ChangeCourse : System.Web.UI.Page
	{
		private DataTransfer transfer = new DataTransfer();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack) bind();
		}

		//GridView绑定数据
		private void bind()
		{
			DataSet ds = transfer.GetTeacherCourseInfo(Session["UserName"].ToString());
			GridView.DataSource = ds.Tables["TeacherCourseInfo"].DefaultView;
			GridView.DataBind();
			ds.Dispose();
		}

		protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
		{
			GridView.EditIndex = e.NewEditIndex;
			bind();
		}

		protected void GridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			string ID = GridView.Rows[e.RowIndex].Cells[0].Text.ToString();
			string Where = ((TextBox)GridView.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString();
			transfer.UpdateTeacherCourse(ID, Session["UserName"].ToString(), Where);
			GridView.EditIndex = -1;
			bind();
		}

		protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			GridView.EditIndex = -1;
			bind();
		}

		protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			string ID = GridView.Rows[e.RowIndex].Cells[0].Text.ToString();
			transfer.DeleteTeacherCourse(ID, Session["UserName"].ToString());
			GridView.EditIndex = -1;
			bind();
		}
	}
}