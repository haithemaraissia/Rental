<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>JqGrid</title>
    <link href="themes/redmond/jquery-ui-1.8.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="themes/ui.jqgrid.css" rel="stylesheet" type="text/css" />    
    <script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.jqGrid.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1">
    <table id="jsonmap">
    </table>
    <div id="pjmap">
    </div>

    
    <script type="text/javascript">
        
        jQuery("#jsonmap").jqGrid({
            url: 'FetchData.aspx',
            datatype: 'json',
            colNames: ['ID', 'Name','Active','Gender'],
            colModel: [{
                    name: 'id',
                    index: 'id',
                    width: 20,
                    editable: false,
                    editoptions: {
                    readonly: true,
                    size: 10
                }},                
                {
                    name: 'name',
                    index: 'name',
                    width: 150,
                    align: "left",
                    editable: true,
                    size: 100
                  },
                {
                    name: 'isClosed',
                    index: 'isClosed',
                    width: 100,
                    align: 'left',
                    editable: true,
                    edittype: "checkbox",
                    editoptions: {
                    value: "true:false",
                    formatter: "checkbox"
                }},
                {
                    name: 'gender',
                    index: 'gender',
                    width: 100,
                    formatter:'select',
                    editable: true,
                    edittype: "select", 
                    editoptions: {value: "0:select;1:male;2:female"}
                }],
                rowNum: 10,
                rowList: [2, 5, 10, 15],
                pager: '#pjmap',
                sortname: 'id',
                sortorder: "desc",
                viewrecords: true, 
                jsonReader: {
                    repeatitems: false
                },
                width: 600,
                caption: 'jqGrid demo',
                height: '100%',
                editurl: 'FetchData.aspx'
            });


            jQuery("#jsonmap").navGrid("#pjmap", {
                    edit: true,
                    add: true,
                    del: true
                },
                {
                    closeAfterEdit: true,
                    reloadAfterSubmit: false
                },
                {
                    closeAfterAdd: true,
                    reloadAfterSubmit: false
                },
                {
                    reloadAfterSubmit: false
                });



    </script>

    </form>
</body>
</html>
