<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewItemReturn.aspx.cs" Inherits="DeltoneCRM.Warehouse.AddNewItemReturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="script/jquery.min.js"></script>
    <script src="script/moment.min.js"></script>



    <%-- <script src="../js/jquery-ui-1.10.3.custom.js"></script>--%>
    <script src="../js/jquery.validate.js" type="text/javascript"></script>
    <link href="../../css/ManageCSS.css" rel="stylesheet" />
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="script/jquery-ui.min.js"></script>
    <link href="script/jquery-ui.min.css" rel="stylesheet" />
     <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <%-- <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>--%>

    <%-- <link href="../css/Overall.css" rel="stylesheet" />--%>

    <style type="text/css">

        div.dataTables_length {
    float: left;
    background-color: cccccc;
}
 
div.dataTables_filter {
    float: right;
    background-color: cccccc;
}
 
div.dataTables_info {
    float: left;
    background-color: cccccc;
}
 
div.dataTables_paginate {
    float: right;
    background-color: cccccc;
}
 
div.dataTables_length,
div.dataTables_filter,
div.dataTables_paginate,
div.dataTables_info {
    padding: 6px;
}
/* Footer cells */ 
table.pretty tfoot th {
    background: #cccccc;
    text-align: left;
}
 
table.pretty tfoot td {
    background: #cccccc;
    text-align: center;
    font-weight: bold;
}
div.dataTables_wrapper {
    background-color: #e2e1e1;
}
        .tbl-top-bg {
            background-color: #e2e1e1;
        }
        .ui-autocomplete {
            z-index: 100;
        }

        .ui-widget-content {
            border: 2px solid #DDD !important;
        }

        .style1 {
            width: 100%;
        }

        body {
            margin-top: 0px;
            margin-bottom: 0px;
            background-color: #eeeeee;
        }

        .tbl-bg {
            background-color: #eeeeee;
        }

        .top-heading {
            height: 30px;
            background-color: #CCCCCC;
            border-bottom-color: #ffffff;
            border-bottom-style: solid;
            border-bottom-width: 2px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 16px;
            color: #ffffff;
            font-weight: 400;
            letter-spacing: -1px;
        }

        .labels-style {
            width: 93px;
            height: 30px;
            color: #666666;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            letter-spacing: -1px;
            border-top-color: #666666;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #666666;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #cccccc;
            padding-left: 5px;
        }

        .txtbox-200-style {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #eeeeee;
            padding-left: 5px;
        }

        .txtbox-200-style-err {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border: 1px solid #f00;
            background-color: #ffffff;
            padding-left: 5px;
            outline: none;
        }

        .txtbox-200-style-edit {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #ffffff;
            padding-left: 5px;
        }

        .drpdwn-200-style-edit {
            width: 215px;
            height: 34px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #ffffff;
            padding-left: 1px;
        }

        .tbl-auto-row-01 {
            height: 25px;
            width: 23px;
            border-top-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-right-width: 1px;
            border-top-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-right-style: solid;
            border-top-color: #999999;
            border-bottom-color: #999999;
            border-left-color: #999999;
            border-right-color: #999999;
            text-align: center;
        }

        .height-15px-style {
            height: 15px;
            font-size: 5px;
        }


        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }

        .btn-green-style {
            width: 125px;
            height: 30px;
            color: #ffffff;
            text-align: center;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            border-top-color: #92c053;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #92c053;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #92c053;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #92c053;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #92c053;
        }

        .btn-red-style {
            width: 125px;
            height: 30px;
            color: #ffffff;
            text-align: center;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            border-top-color: #f26d4e;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #f26d4e;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #f26d4e;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #f26d4e;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #f26d4e;
        }

            .btn-red-style:hover {
                background-color: #fc3b36;
                cursor: pointer;
            }

        .btn-green-style:hover {
            background-color: #6ebb04;
            cursor: pointer;
        }

        .auto-style1 {
            width: 680px;
        }

        .auto-style2 {
            width: 265px;
        }

        .styledrop {
            width: 100px;
            height: 30px;
        }

        .ui-dialog {
            border: 1px solid #aed0ea;
        }

          .spanStyleLink {
            cursor: pointer;
            color: blue;
            text-decoration: underline;
            font-style: normal;
            font-size: 16px;
            text-align: center;
        }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css' />

    <script type="text/javascript">



        function blockChar(ev) {
            var keynum;
            if (window.event) keynum = ev.keyCode;// IE
            else if (ev.which) keynum = ev.which; // Other browsers
            else keynum = ev.charCode;
            if (keynum == 44) { return false; }

        }





    </script>
</head>
<body>
    <form id="form1" runat="server">
     

       <div>
            <table id="tblproductItems" width="300" border="0" cellpadding="0" cellspacing="0">
                        </table>
            <asp:HiddenField ID="rownamehidden" runat="server" />
        <table width="780" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                    </tr>

                    <tr>
                      <td bgcolor="#FFFFFF"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">ITEM CODE&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewItemCode" type="text" class="txtbox-200-style-edit" id="NewItemCode" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">Name</td>
                                  <td width="200">
                                      <input name="warehouseitemtrayname" type="text" class="txtbox-200-style-edit" id="warehouseitemtrayname" />
                                    </td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                                                  <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DESCRIPTION</td>
                                  <td width="215"><input name="NewDescription" type="text" class="txtbox-200-style-edit" id="NewDescription" onkeypress="return blockChar(event)" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">COG</td>
                                  <td width="200"><input name="NewCOG" type="text" class="txtbox-200-style-edit" id="NewCOG" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>


                                                  <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                                                  <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">
                                  <table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">Quantity</td>
                                  <td width="215"><input name="Quantity" type="text" class="txtbox-200-style-edit" id="Quantity"  /></td>
                                </tr>
                              </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">RESELL PRICE&nbsp;</td>
                                  <td width="215"><input name="NewResellPrice" type="text" class="txtbox-200-style-edit" id="NewResellPrice" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                                                  <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                                                  <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BEST PRICE&nbsp;</td>
                                  <td width="215"><input type="checkbox" id="chk_bestprice" class="txtbox-200-style-edit"/></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">OFTEN FAULTY&nbsp;</td>
                                  <td width="215"><input type="checkbox" id="chk_faulty" class="txtbox-200-style-edit"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                          
                                                  <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                            <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LOCATION COLUMN&nbsp;</td>
                                  <td width="215"> <asp:DropDownList ID="warehouselocationdropdownlist" CssClass="styledrop" runat="server"></asp:DropDownList>
                        (Column Name)</td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LOCATION ROW&nbsp;</td>
                                  <td width="215">  <asp:DropDownList ID="warehouselocationrowdropdownlist" CssClass="styledrop" runat="server"></asp:DropDownList>
                        (Row Number)</td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                          <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">OEM&nbsp;</td>
                                  <td width="215"> <select id="wareoemtype" style="width: 100px; height: 30px;">
                           
                            <option value="GENUINE">GENUINE  </option>
                            <option value="COMPATIBLE">COMPATIBLE         </option>


                        </select></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DAMAGED&nbsp;</td>
                                  <td width="215">  <input name="warehouseitemdamaged" type="checkbox" class="txtbox-200-style-edit" id="warehouseitemdamaged" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>

                          </tr>
                             <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LENGTH&nbsp;</td>
                                  <td width="215"> <input name="warehouseitemlength" type="text" class="txtbox-200-style-edit" id="warehouseitemlength" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">WIDTH&nbsp;</td>
                                  <td width="215"> <input name="warehouseitemwidth" type="text" class="txtbox-200-style-edit" id="warehouseitemwidth" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>

                          </tr>
                             <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">HEIGHT&nbsp;</td>
                                  <td width="215"><input name="warehouseitemheight" type="text" class="txtbox-200-style-edit" id="warehouseitemheight" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">WEIGHT&nbsp;</td>
                                  <td width="215"> <input name="warehouseitemweight" type="text" class="txtbox-200-style-edit" id="warehouseitemweight" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>

                          </tr>
                              <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DSB&nbsp;</td>
                                  <td width="215"><input type="text" class="txtbox-200-style-edit" id="txt_dsb" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BOXING&nbsp;</td>
                                  <td width="215"> <input name="warehouseitemboxing" type="text" class="txtbox-200-style-edit" id="warehouseitemboxing" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>

                          </tr>
                            <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">NOTES&nbsp;</td>
                                  <td width="215"><input type="text" class="txtbox-200-style-edit" id="warehouseitemnotes" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BRAND&nbsp;</td>
                                  <td width="215"> <input name="warehouseitembrand" type="text" class="txtbox-200-style-edit" id="warehouseitembrand" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                          </tr>              <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>

                          <tr>
   <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">TYPE&nbsp;</td>
                                  <td width="215"><input type="text" class="txtbox-200-style-edit" id="warehouseitemtype" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <%-- <td width="100" class="labels-style">BRAND&nbsp;</td>
                                  <td width="215"> <input name="warehouseitembrand" type="text" class="txtbox-200-style-edit" id="warehouseitembrand" /></td>--%>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                          </tr>              <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>


                              <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>

                        </table></td>
                    </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td style="display:none;"><input name="button" type="reset" class="settingsbutton_gray" id="button" value="CLEAR FORM" /></td>
                      </tr>
                    <tr>
                      <td>
                          <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px">&nbsp;</td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px">
                                                  <input name="createrecord" type="submit" onclick="return submitItemsData();" class="btn-green-style" id="createrecord" value="SAVE" /></td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>
                        </td>
                      </tr>
              <tr>
                    <td></td>
                    <td>
                        
                    </td>
                </tr>

                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    
                    </table>
    </div>

        <div id="prohistoryDiv" style="display:none;">
         <table>
                <tr>
                    <td><span id="showSpan" class="spanStyleLink">Show History</span>
                        <span id="hideSpan" class="spanStyleLink" style="display: none;">Hide History</span>
                    </td>
                </tr>
                <tr id="historyheader" style="display: none;">
                    <td class="section_headings">History</td>
                </tr>
                <tr id="historytr" style="display: none;">
                    <td >
                        <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="display" id="tblhistory">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Action</th>
                                                <th>Created Date</th>
                                                <th>Action By</th>
                                                <th>Before Change</th>
                                                 <th>After Change</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table></div>
    </form>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript">

        var templateRow = "<tr class=\"item-row\" id=\"#trselectedrow#\"><td > <span name=\"itemcodenamespan\" style=\"width:80px;\"  id=\"#itemcodespan#\">#pro_codeReplace#</span></td><td ><input name=\"itemnameQtrtxt\" style=\"width:45px;\" type=\"text\"  id=\"#itemQtrtxt#\"/></td> " +
                                       "<td class=\"tbl-auto-row-01\"><a class=\"delete\" style=\"cursor:pointer\"  onclick=\"removeThisDiv('#current_tr#');\" title=\"Remove row\"><img src=\"../Images/x.png\" width=\"6\" height=\"16\" /></td> "
                                    + "</tr>";
        var count = 0;
        var spanId = "spanId_";
        var qtytxt = "qtytxt_";
        var trTableRow = "trrow_";
        var allWItemQuantity;
        var mainDisplayWShelfItemDetails;
        var allDisplayWShelfItemDetails = new Array();
       
        function CallItemCode() {


            $('#warehouseitemcode').autocomplete({

                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        url: "../Reports/CustomersBYProduct.aspx/GetProductCode",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                //appendRow(item);
                                return {

                                    value: item
                                }
                            }));
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                        }
                    });
                },
                //select: function (e, i) {

                //    appendRow(i.item.value);



                //},
            });

        }

        function appendRow($row) {
            console.log($row);
            count = count + 1;
            var spanProId = spanId + count;
            var qtyProId = qtytxt + count;
            var trRow = trTableRow + count;
            var valtem = templateRow;
            // valtem = valtem.replace("#itemcodenamespan#", spanProId);
            valtem = valtem.replace("#itemcodespan#", spanProId);
            valtem = valtem.replace("#trselectedrow#", trRow);
            valtem = valtem.replace("#current_tr#", trRow);
            valtem = valtem.replace("#itemQtrtxt#", qtyProId);
            // valtem = valtem.replace("#itemnameQtrtxt#", qtyProId);
            valtem = valtem.replace("#pro_codeReplace#", $row);
            $('#tblproductItems').append(valtem);
            $('#warehouseitemcode').val('');
        }


        function addTableRow() {
            var coePro = $('#warehouseitemcode').val();
            if (coePro == "") {
                alert("Please enter product code");
                return;
            }

            var checkOEMType = $('#wareoemtype').val();
            if (checkOEMType == "GENUINE") {
                var trCount = $('#tblproductItems tr').length;
                if (trCount > 0) {
                    alert("You can add only one product for GENUINE OEM");
                    return;
                }
            }
            appendRow(coePro);

        }


        function removeThisDiv(id) {
            $("#tblproductItems").find("#" + id).remove();
            // $(this).closest('tr').remove();


        }

        function DisplayWShelfItemDetails() {

            this.Id = 0;
            this.ItemId = 0;
            this.Name = "";
            this.OEMCode = "";
            this.LocationId = 0;
            this.Length = "";
            this.Width = "";
            this.Height = "";
            this.Weight = "";
            this.Boxing = "";
            this.PriceLock = false;
            this.ReportedFaulty = false;
            this.Damaged = false;
            this.LocationColumnName = "";
            this.Notes = "";
            this.COG = "";
            this.ManagerUnitPrice = "";
            this.RepUnitPrice = "";
            this.Quantity = "";
            this.DSB = "";
            this.SupplierItemCode = "";
            this.Description = "";
            this.Type = "";
            this.Brand = "";
        }


        function WItemQuantity() {
            this.Id = 0;
            this.ShelfItemId = 0;
            this.Isremoved = 0;
            this.Qty = 0;
            this.ProductCode = "";
            this.SupplierName = "";

        }




        function callFinalCollectionObjects() {
            
                createMainObject();
                return true;
          

            
        }

        function createMainObject() {

             mainDisplayWShelfItemDetails = new DisplayWShelfItemDetails();

            mainDisplayWShelfItemDetails.OEMCode = $('#wareoemtype').val();
            mainDisplayWShelfItemDetails.Name = $('#warehouseitemtrayname').val();
            mainDisplayWShelfItemDetails.LocationId = parseInt($('#warehouselocationrowdropdownlist').val());
            mainDisplayWShelfItemDetails.Length = $('#warehouseitemlength').val();
            mainDisplayWShelfItemDetails.Width = $('#warehouseitemwidth').val();
            mainDisplayWShelfItemDetails.Height = $('#warehouseitemheight').val();
            mainDisplayWShelfItemDetails.Weight = $('#warehouseitemweight').val();
            mainDisplayWShelfItemDetails.Boxing = $('#warehouseitemboxing').val();
            if ($("#warehouseitemdamaged").is(':checked'))
                mainDisplayWShelfItemDetails.Damaged = true;

            if ($("#chk_bestprice").is(':checked'))
                mainDisplayWShelfItemDetails.PriceLock = true;
            if ($("#chk_faulty").is(':checked'))
                mainDisplayWShelfItemDetails.ReportedFaulty = true;

            mainDisplayWShelfItemDetails.SupplierItemCode = $('#NewItemCode').val();
            mainDisplayWShelfItemDetails.Description = $('#NewDescription').val();
            mainDisplayWShelfItemDetails.COG = $('#NewCOG').val();
            mainDisplayWShelfItemDetails.Quantity = $('#Quantity').val();
            mainDisplayWShelfItemDetails.ManagerUnitPrice = $('#NewCOG').val();
            mainDisplayWShelfItemDetails.RepUnitPrice = $('#NewResellPrice').val();
            mainDisplayWShelfItemDetails.Notes = $('#warehouseitemnotes').val();
            mainDisplayWShelfItemDetails.DSB = $('#txt_dsb').val();
            mainDisplayWShelfItemDetails.Type = $('#warehouseitemtype').val();
            mainDisplayWShelfItemDetails.Brand = $('#warehouseitembrand').val();

        }

        $(document).ready(function () {

            var selectedShelId = parent.selectedShelId;
            if (selectedShelId != "") {
                console.log(selectedShelId);
                $("#prohistoryDiv").show();
            }
            else {
                $("#prohistoryDiv").hide();
            }

            
            loadShelfItems();

            var hiddenRow = $("#rownamehidden").val();
           // console.log(hiddenRow);

            setRowBasedOnColumnSelection();

            $("#hideSpan").click(function () {
                $("#hideSpan").hide();
                $("#showSpan").show();
                $("#historytr").hide();
                $("#historyheader").hide();
            });

            callHistoryFunction();

        });

        var table = "";
        function callHistoryFunction() {
           
            $("#showSpan").click(function () {
                $("#showSpan").hide();
                $("#hideSpan").show();
                $("#historytr").show();
                $("#historyheader").show();
                if (table)
                    table.destroy();

                table =
                $('#tblhistory').dataTable({

                    "ajax": "../Manage/Process/FetchProductAudit.aspx?itemId=" + editItemId,
                    "columnDefs": [
                     { className: 'align_left', "targets": [1, 2] },
                     { className: 'align_center', "targets": [3] },

                    ],
                    "order": [[0, "desc"]],
                    "iDisplayLength": 25,
                });

            });


        }

       

        function submitItemsData() {

                callFinalCollectionObjects();

                var selectedShelId = parent.selectedShelId;
                if (selectedShelId != "") {
                    mainDisplayWShelfItemDetails.Id = parseInt(selectedShelId);
                }
                console.log(mainDisplayWShelfItemDetails);

                $("#<%=form1.ClientID%>").validate({
                    onfocusout: false,
                    onkeyup: false,
                    rules: {
                        NewItemCode:{
                            required: true,
                        }, NewDescription: {
                            required: true,
                        },
                        warehouseitemtrayname: {
                            required: true,
                        },
                       
                        NewCOG:{
                            required: true,
                        },
                        NewResellPrice:{
                            required: true,
                        }
                    },
                    messages:
                        {
                            warehouseitemtrayname: {
                                required: "Please Enter Name"
                            },
                            
                            NewCOG: {
                                required: "Please Enter COG"
                            },
                            NewResellPrice: {
                                required: "Please Enter Resell Price"
                            },
                            NewDescription: {
                                required: "Please Enter Description"
                            }
                            ,
                            NewItemCode: {
                                required: "Please Enter ItemCode"
                            }

                        },
                    highlight: function (element) {
                        $(element).closest("input")
                        .addClass("txtbox-200-style-err")
                        .removeClass("txtbox-200-style-edit");
                    },
                    unhighlight: function (element) {
                        $(element).closest("input")
                        .removeClass("txtbox-200-style-err")
                        .addClass("txtbox-200-style-edit");
                    },
                    errorPlacement: function (error, element) {

                        error.insertAfter(element);
                    },
                    success: function (label) {
                        //alert('Triggered');

                    },

                    submitHandler: function (form) {
                        $.ajax({
                            type: "POST",
                            url: "Process/addshelfreturnitems.aspx",
                            data: JSON.stringify(mainDisplayWShelfItemDetails),
                            success: function (msg) {
                                //console.log(msg);
                                if (msg == "1") {
                                    alert('Items successfully created');
                                }
                                else {
                                    alert('Error Occurred');
                                }

                                window.parent.closeAddFrame();
                                window.parent.location.reload(false);
                            },
                            error: function (xhr, err) {
                                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                alert("responseText: " + xhr.responseText);
                            },
                        });

                    },


                });

            }

        function EditLoadCreditItem() {
            this.ProductCode = "";
        }

        function loadShelfItems() {
            console.log(parent.productCode);
            var selectedShelId = parent.productCode;
            if (selectedShelId != "") {
               
                var objPass = new EditLoadCreditItem();
                objPass.ProductCode ="DS"+ selectedShelId;
                $.ajax({
                    type: "POST",
                    url: "Fetch/fetcheditreturnitem.aspx",
                    data: JSON.stringify(objPass),
                    success: function (msg) {
                        var obj = JSON.parse(msg);
                        //console.log(obj);
                        count = 0;
                        setLoadedValues(obj);
                    },
                    error: function (xhr, err) {
                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                        alert("responseText: " + xhr.responseText);
                    },
                });

            }

        }

        var editItemId = 0;

        function setLoadedValues(dataEdit) {
            editItemId = dataEdit.ItemId;
            if (editItemId > 0) {
                $('#wareoemtype').val(dataEdit.OEMCode);

                $('#warehouseitemlength').val(dataEdit.Length);
                $('#warehouseitemwidth').val(dataEdit.Width);
                $('#warehouseitemheight').val(dataEdit.Height);
                $('#warehouseitemweight').val(dataEdit.Weight);
                $('#warehouseitemboxing').val(dataEdit.Boxing);

                //  $('#NewItemCode').val(dataEdit.SupplierItemCode);
                // $('#NewDescription').val(dataEdit.Description);
                // $('#NewCOG').val(dataEdit.COG);
                // $('#Quantity').val(dataEdit.Quantity);
                // $('#NewResellPrice').val(dataEdit.ManagerUnitPrice);
                // $('#NewResellPrice').val(dataEdit.RepUnitPrice);
                $('#warehouseitemnotes').val(dataEdit.Notes);
                $('#txt_dsb').val(dataEdit.DSB);
                $('#warehouseitemtype').val(dataEdit.Type);
                $('#warehouseitembrand').val(dataEdit.Brand);


                $("#warehouselocationdropdownlist option:contains(" + dataEdit.LocationColumnName + ")").attr('selected', 'selected');

                $('#warehouseitemtrayname').val(dataEdit.Name);
                $("#warehouseitemdamaged").prop('checked', dataEdit.Damaged);
                $("#chk_faulty").prop('checked', dataEdit.ReportedFaulty);
                $("#chk_bestprice").prop('checked', dataEdit.PriceLock);



                setDropbasedONCol(dataEdit.LocationId, dataEdit.LocationColumnName);
            }
        }

        function setDropbasedONCol(locationId, colName) {
            var hiddenRow = JSON.parse($("#rownamehidden").val());
        
            var selectedColName = $('#warehouselocationdropdownlist :selected').text();;
           
            $("#warehouselocationrowdropdownlist").empty();
            for (var i = 0; i < hiddenRow.length; i++) {
                var obj = hiddenRow[i];
             
                if (obj.ColName == selectedColName) {
                    $("#warehouselocationrowdropdownlist").append(
        $('<option></option>').val(obj.Id).html(obj.RowName)
    );
                   
                }
            }

            $('#warehouselocationrowdropdownlist').val(locationId);
        }

        function appendRowEdit(prodCode, qty) {

            count = count + 1;
            var spanProId = spanId + count;
            var qtyProId = qtytxt + count;
            var trRow = trTableRow + count;
            var valtem = templateRow;
            // valtem = valtem.replace("#itemcodenamespan#", spanProId);
            valtem = valtem.replace("#itemcodespan#", spanProId);
            valtem = valtem.replace("#trselectedrow#", trRow);
            valtem = valtem.replace("#current_tr#", trRow);
            valtem = valtem.replace("#itemQtrtxt#", qtyProId);
            // valtem = valtem.replace("#itemnameQtrtxt#", qtyProId);
            valtem = valtem.replace("#pro_codeReplace#", prodCode);
            $('#tblproductItems').append(valtem);
            $("#" + qtyProId).val(qty);
        }

        function EditMode() {
            this.Id = ""
        }

        function LocationManageRow() {
            this.Id = "",
             this.RowName = ""

        }

        function setRowBasedOnColumnSelection() {

            var hiddenRow = JSON.parse($("#rownamehidden").val());

            $("#warehouselocationdropdownlist").change(function () {
                var selectedColName = $('#warehouselocationdropdownlist :selected').text();;
              
                $("#warehouselocationrowdropdownlist").empty();
                for (var i = 0; i < hiddenRow.length; i++) {
                    var obj = hiddenRow[i];
                   
                    if (obj.ColName == selectedColName) {
                        $("#warehouselocationrowdropdownlist").append(
            $('<option></option>').val(obj.Id).html(obj.RowName)
        );
                    }
                }

            });
        }



    </script>
</body>

</html>

