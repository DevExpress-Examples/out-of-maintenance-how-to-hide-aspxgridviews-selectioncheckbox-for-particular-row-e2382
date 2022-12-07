<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function cbCheckAll_CheckedChanged(s, e) {
            grid.PerformCallback(s.GetChecked().toString());
        }

        function cbCheck_CheckedChanged(s, e, index) {
            grid.SelectRowOnPage(index, s.GetChecked());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" AutoGenerateColumns="False"
            DataSourceID="AccessDataSource1" KeyFieldName="CategoryID" 
            OnCustomCallback="grid_CustomCallback">
            <Columns>
                <dx:GridViewDataTextColumn Caption="#" VisibleIndex="0">
                    <HeaderTemplate>
                        <dx:ASPxCheckBox ID="cbCheckAll" runat="server" OnInit="cbCheckAll_Init" >
                        </dx:ASPxCheckBox>
                    </HeaderTemplate>
                    <DataItemTemplate>
                        <dx:ASPxCheckBox ID="cbCheck" runat="server" OnInit="cbCheck_Init">
                        </dx:ASPxCheckBox>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="5"></SettingsPager>
        </dx:ASPxGridView>
    </div>
    <asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/nwind.mdb"
        SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
    </asp:AccessDataSource>
    </form>
</body>
</html>