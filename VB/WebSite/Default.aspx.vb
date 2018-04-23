Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim cb As ASPxCheckBox = CType(sender, ASPxCheckBox)
		cb.ClientSideEvents.CheckedChanged = String.Format("function (s, e) {{ grid.PerformCallback(s.GetChecked().toString()); }}")
	End Sub
	Protected Sub cbCheck_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim cb As ASPxCheckBox = CType(sender, ASPxCheckBox)
		Dim container As GridViewDataItemTemplateContainer = CType(cb.NamingContainer, GridViewDataItemTemplateContainer)

		cb.ClientInstanceName = String.Format("cbCheck{0}", container.VisibleIndex)
		cb.Checked = grid.Selection.IsRowSelected(container.VisibleIndex)
		cb.ClientSideEvents.CheckedChanged = String.Format("function (s, e) {{ grid.SelectRowOnPage({0}, s.GetChecked()); }}", container.VisibleIndex)

		Dim row As DataRow = grid.GetDataRow(container.VisibleIndex)
		cb.Visible = IsCheckBoxVisibleCriteria(row)
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		Dim needToSelectAll As Boolean = False
		Boolean.TryParse(e.Parameters, needToSelectAll)

		Dim gridView As ASPxGridView = CType(sender, ASPxGridView)

		Dim startIndex As Integer = gridView.PageIndex * gridView.SettingsPager.PageSize
		Dim endIndex As Integer = Math.Min(gridView.VisibleRowCount, startIndex + gridView.SettingsPager.PageSize)

		For i As Integer = startIndex To endIndex - 1
			If needToSelectAll Then
				Dim cb As ASPxCheckBox = CType(gridView.FindRowCellTemplateControl(i, CType(gridView.Columns("#"), GridViewDataColumn), "cbCheck"), ASPxCheckBox)
				Dim row As DataRow = gridView.GetDataRow(i)
				gridView.Selection.SetSelection(i, IsCheckBoxVisibleCriteria(row))
			Else
				gridView.Selection.SetSelection(i, needToSelectAll)
			End If
		Next i
	End Sub
	Private Function IsCheckBoxVisibleCriteria(ByVal row As DataRow) As Boolean
		Return Not row("CategoryName").ToString().Contains("a")
	End Function
End Class