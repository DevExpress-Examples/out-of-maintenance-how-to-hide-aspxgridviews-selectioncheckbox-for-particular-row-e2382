' Developer Express Code Central Example:
' How to hide ASPxGridView's SelectionCheckBox for particular row
' 
' This example is based on the http://www.devexpress.com/scid=E1559 example. It
' illustrate how to emulate the ASPxGridView's SelectionCheckBox and hide it for
' particular ASPxGridView's DataRow based on the DataRow's values.
' See
' Also:
' http://www.devexpress.com/scid=E1559
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2382

Imports System
Imports System.Data
Imports DevExpress.Web

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Private needToSelectAll As Boolean
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = needToSelectAll
    End Sub
    Protected Sub cbCheck_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        Dim container As GridViewDataItemTemplateContainer = CType(cb.NamingContainer, GridViewDataItemTemplateContainer)

        cb.ClientInstanceName = String.Format("cbCheck{0}", container.VisibleIndex)
        cb.Checked = grid.Selection.IsRowSelected(container.VisibleIndex)
        cb.ClientSideEvents.CheckedChanged = String.Format("function (s, e) {{ cbCheck_CheckedChanged(s,e, {0}); }}", container.VisibleIndex)

        Dim row As DataRow = grid.GetDataRow(container.VisibleIndex)
        cb.Visible = IsCheckBoxVisibleCriteria(row)
    End Sub
    Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        needToSelectAll = False
        Boolean.TryParse(e.Parameters, needToSelectAll)

        Dim gridView As ASPxGridView = DirectCast(sender, ASPxGridView)

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