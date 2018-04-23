using System;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class _Default : System.Web.UI.Page {
    protected void cbCheckAll_Init(object sender, EventArgs e) {
        ASPxCheckBox cb = (ASPxCheckBox)sender;
        cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ grid.PerformCallback(s.GetChecked().toString()); }}");
    }
    protected void cbCheck_Init(object sender, EventArgs e) {
        ASPxCheckBox cb = (ASPxCheckBox)sender;
        GridViewDataItemTemplateContainer container = (GridViewDataItemTemplateContainer)cb.NamingContainer;

        cb.ClientInstanceName = string.Format("cbCheck{0}", container.VisibleIndex);
        cb.Checked = grid.Selection.IsRowSelected(container.VisibleIndex);
        cb.ClientSideEvents.CheckedChanged = string.Format("function (s, e) {{ grid.SelectRowOnPage({0}, s.GetChecked()); }}", container.VisibleIndex);

        DataRow row = grid.GetDataRow(container.VisibleIndex);
        cb.Visible = IsCheckBoxVisibleCriteria(row);
    }
    protected void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
        bool needToSelectAll = false;
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