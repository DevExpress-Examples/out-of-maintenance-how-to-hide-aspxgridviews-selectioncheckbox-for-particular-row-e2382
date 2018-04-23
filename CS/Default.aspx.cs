// Developer Express Code Central Example:
// How to hide ASPxGridView's SelectionCheckBox for particular row
// 
// This example is based on the http://www.devexpress.com/scid=E1559 example. It
// illustrate how to emulate the ASPxGridView's SelectionCheckBox and hide it for
// particular ASPxGridView's DataRow based on the DataRow's values.
// See
// Also:
// http://www.devexpress.com/scid=E1559
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2382

using System;
using System.Data;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page {
	bool needToSelectAll;
    protected void cbCheckAll_Init(object sender, EventArgs e) {
        ASPxCheckBox cb = (ASPxCheckBox)sender;
		cb.ClientSideEvents.CheckedChanged = string.Format("cbCheckAll_CheckedChanged");
		cb.Checked = needToSelectAll;
    }
    protected void cbCheck_Init(object sender, EventArgs e) {
        ASPxCheckBox cb = (ASPxCheckBox)sender;
        GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)cb.NamingContainer;

        cb.ClientInstanceName = string.Format("cbCheck{0}", container.VisibleIndex);
        cb.Checked = grid.Selection.IsRowSelected(container.VisibleIndex);
		cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ cbCheck_CheckedChanged(s,e, {0}); }}", container.VisibleIndex);

        DataRow row = grid.GetDataRow(container.VisibleIndex);
        cb.Visible = IsCheckBoxVisibleCriteria(row);
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        needToSelectAll = false;
        bool.TryParse(e.Parameters, out needToSelectAll);

        ASPxGridView gridView = (ASPxGridView)sender;

        int startIndex = gridView.PageIndex * gridView.SettingsPager.PageSize;
        int endIndex = Math.Min(gridView.VisibleRowCount, startIndex + gridView.SettingsPager.PageSize);

        for (int i = startIndex; i < endIndex; i++) {
            if (needToSelectAll) {
                ASPxCheckBox cb = (ASPxCheckBox)gridView.FindRowCellTemplateControl(i, (GridViewDataColumn)gridView.Columns["#"], "cbCheck");
                DataRow row = gridView.GetDataRow(i);
                gridView.Selection.SetSelection(i, IsCheckBoxVisibleCriteria(row));
            } else
                gridView.Selection.SetSelection(i, needToSelectAll);
        }
    }
    private bool IsCheckBoxVisibleCriteria(DataRow row) {
        return !row["CategoryName"].ToString().Contains("a");
    }
}