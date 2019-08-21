<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintCreditNote.aspx.cs" Inherits="DeltoneCRM.Print.PrintCreditNote" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
            <title>PRINT CREDIT NOTE</title>
            <style type="text/css">
            .width-980-style {
                width: 980px;
            }
            .width-940-style {
                width: 940px;
            }
            .width-770-style {
                width: 770px;
            }
            .width-235-style {
                width: 235px;
            }
            .width-470-style {
                width: 470px;
            }
            .width-250-style {
                width: 250px;
            }
            .top-table-style {
                width: 570px;
            }
            .height-10px-style {
                height: 10px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 5px !important;
            }
            .height-15px-style {
                height: 15px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 5px !important;
            }
            .align-vertical {
                vertical-align: top;    
            }
            .white-box-outline-top {
                 width: 810px;
                 /*border: 1px solid #cccccc;
                 background-color: #ffffff;*/
                }
            .total {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px !important;
                color: #494949;
                }
            .cogtotal {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px !important;
                color: #494949;
                }
            .notes-textbox-style {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px !important;
                color: #494949;
                width: 928px;
                resize: none;
                padding: 5px 5px 5px 5px;
                height: 75px;
                }
            .top-payment-terms-heading {
                width: 150px;
                height: 30px;
                background-color: #CCCCCC;
                border-bottom-color: #ffffff;
                border-bottom-style: solid;
                border-bottom-width: 2px;
                text-align: left;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #494949;
                font-weight: 400;
                letter-spacing: -1px;
            }
            .all-headings-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 28px;
                color: #000000;
                font-weight: 400;
                letter-spacing: -1px;
                width: 470px;
            }

             .Title_Style 
              {
                font-family: 'Droid Sans', sans-serif;
                font-size: 30px;
                color:red;
                font-weight: 400;
                letter-spacing: -1px;
                width: 470px;
                opacity:0.5;

              }


            .acc-owner-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 18px;
                color: #eb473d;
                font-weight: 400;
                letter-spacing: -1px;
                width: 470px;
                text-align: right;
            }
            .all-subheadings-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 18px;
                color: #000000;
                font-weight: 400;
                letter-spacing: -1px;
            }
            .company-name-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 24px;
                color: #000000;
                font-weight: 400;
                letter-spacing: -1px;
                /*vertical-align: top;*/
                border-bottom-style: solid;
                border-bottom-color: #000000;
                border-bottom-width: 2px;
                height: 30px;
            }
            .company-details-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 16px;
                color: #000000;
                font-weight: 400;
                letter-spacing: -1px;
                height: 25px;
            }
            .company-details-delivery-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 16px;
                color: #000000;
                font-weight: 400;
                letter-spacing: -1px;
                height: 20px;
            }
            .company-details-tel-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
                color: #494949;
                font-weight: 400;
                letter-spacing: -1px;
                height: 20px;
            }
            .test-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 13px;
                color: #ffffff;
                font-weight:400;
                letter-spacing: -0.05em;
            }
            .white-bg-style {
                /*background-color: #ffffff;
                border: 1px solid #CCCCCC;*/
            }
            .white-bg-no-brdr-style {
                background-color: #ffffff;
            }
            .delivery-add-bg-style {
                background-color: #eeeeee;
                width: 290px;
                vertical-align: top;
            }
            .delivery-add-heading-style {
                -webkit-print-color-adjust: exact;
                font-family: 'Droid Sans', sans-serif;
                font-size: 24px;
                color: #000000;
                font-weight:400;
                letter-spacing: -0.05em;
                background-color: #ffffff;
                height: 30px;
                border-bottom-color: #000000;
                border-bottom-style: solid;
                border-bottom-width: 2px;
            }
            .commission-bg-style {
                background-color: #ffffff;
                border: 1px solid #eb473d;
                height: 50px;
            }
            .add-credit-note-btn {
                width: 150px;
                height: 30px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #eb473d;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 1px solid #eb473d;
                background-color: #ffffff;
            }
            .add-credit-note-btn:hover {
                color: #ffffff;
                border: 1px solid #eb473d;
                background-color: #eb473d;
                cursor: pointer;
            }
            .submit-btn {
                width: 171px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #eb473d;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 1px solid #eb473d;
                background-color: #ffffff;
            }
            .submit-btn:hover {
                color: #ffffff;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 1px solid #eb473d;
                background-color: #eb473d;
                cursor: pointer;
            }
            .add-btn {
                width: 100px;
                height: 20px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #eb473d;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 1px solid #eb473d;
                background-color: #ffffff;
            }
            .add-btn:hover {
                color: #ffffff;
                background-color: #eb473d;
                cursor:pointer;
            }
            .payment-terms-drop {
                width: 150px;
                height: 30px;
                margin-bottom: -1px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 2px solid #cde9fe;
                -webkit-appearance: none;
                text-align:center;
                outline: none;
                background-color: #cde9fe;
            }
            .account-owner-drop {
                width: 150px;
                height: 30px;
                margin-bottom: -1px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
                border: 1px solid #cde9fe;
                outline: none;
                -webkit-appearance: none;
                padding-left: 2px;
                background-color: #cde9fe;
            }
            .top-tbl01-style {
                -webkit-print-color-adjust: exact;
                background-color: #cccccc;
                width: 188px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align: center;
                font-weight:400;
                letter-spacing: -0.05em;
	            border: 1px solid #000000;
            }
            .top-tbl02-style {
                -webkit-print-color-adjust: exact;
                background-color: #cccccc;
                width: 188px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align: center;
                font-weight:400;
                letter-spacing: -0.05em;
                border-top-width: 1px;
	            border-right-width: 1px;
                border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-top-color: #000000;
	            border-right-color: #000000;
                border-bottom-color: #000000;
            }
            .top-tbl03-style {
                width: 188px;
                height: 30px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
                color: #000000;
                text-align: center;
                font-weight:400;
                letter-spacing: -0.05em;
                border-left-width: 1px;
	            border-right-width: 1px;
                border-bottom-width: 1px;
	            border-left-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #000000;
                border-bottom-color: #000000;
                border-left-color: #000000;
            }
            .top-tbl04-style {
                width: 188px;
                height: 30px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
                color: #000000;
                text-align: center;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
                border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #000000;
                border-bottom-color: #000000;
            }
            .tbl01-clm01-style {
                -webkit-print-color-adjust: exact;
                width: 23px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:center;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
                border-left-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
                border-left-color: #ffffff;
                border-bottom-color: #000000;
                background:#CCCCCC;
            }
            .tbl01-clm02-style {
                -webkit-print-color-adjust: exact;
                width: 399px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-left: 5px;
            }
            .tbl01-clm03-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-left: 5px;
            }
            .tbl01-clm04-style {
                -webkit-print-color-adjust: exact;
                width: 94px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-left: 5px;
            }
            .tbl01-clm05-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl01-clm06-style {
                -webkit-print-color-adjust: exact;
                width: 29px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl01-clm07-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl01-clm08-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl01-clm09-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #cccccc;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl-auto-row-01 {
	            height: 25px;
                width: 23px;
	            border-bottom-width: 1px;
                border-left-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-left-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-left-color: #ffffff;
                border-right-color: #ffffff;
                text-align:center;
            }
            .tbl-auto-row-01-btm {
	            height: 25px;
                width: 23px;
	            border-bottom-width: 1px;
                border-left-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-left-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-left-color: #ffffff;
                border-right-color: #ffffff;
                text-align:center;
            }
            .tbl-auto-row-02 {
	            height: 25px;
                width: 404px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            /* border-top-color: #666666;
	            border-top-style: solid;
                border-top-width: 1px; */
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
            }
            .tbl-auto-row-02-btm {
	            height: 25px;
                width: 404px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
            }
            .tbl-auto-row-02-inside {
	            height: 23px;
                width: 397px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                padding-left: 5px;
            }
            .tbl-auto-row-03 {
	            height: 25px;
                width: 74px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
            }
            .tbl-auto-row-03-btm {
	            height: 25px;
                width: 74px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
            }
            .tbl-auto-row-03-inside {
	            height: 23px;
                width: 67px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                padding-left: 5px;
            }
            .tbl-auto-row-04 {
	            height: 25px;
                width: 99px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-04-btm {
	            height: 25px;
                width: 99px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-04-inside {
	            height: 23px;
                width: 92px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                padding-left: 5px;
            }
            .tbl-auto-row-05 {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-05-btm {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-05-inside {
	            height: 23px;
                width: 74px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
            }
            .tbl-auto-row-06 {
	            height: 25px;
                width: 34px;
                text-align: right;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-06-btm {
	            height: 25px;
                width: 34px;
                text-align: right;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-06-inside {
	            height: 23px;
                width: 25px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
            }
            .tbl-auto-row-07 {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-07-btm {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-auto-row-07-inside {
	            height: 23px;
                width: 74px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
            }
            .tbl-auto-row-08 {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #494949;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                padding-right: 5px;
                background-color: #eeeeee;
            }
            .tbl-auto-row-08-btm {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #494949;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                padding-right: 5px;
                background-color: #eeeeee;
            }
            .tbl-auto-row-09 {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                padding-right: 5px;
                background-color: #eeeeee;
            }
            
            .tbl-auto-row-09-btm {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                padding-right: 5px;
                background-color: #eeeeee;
            }

            .tbl02-clm01-style {
                -webkit-print-color-adjust: exact;
                width: 783px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
                border-left-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
                border-left-color: #cccccc;
                border-bottom-color: #000000;
                background:#CCCCCC;
                padding-left: 5px;
            }
            .tbl02-clm01-style-supp {
                -webkit-print-color-adjust: exact;
                width: 783px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
                border-left-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
                border-left-color: #cccccc;
                border-bottom-color: #000000;
                background:#CCCCCC;
                padding-left: 5px;
            }
            .tbl02-clm02-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl02-clm02-style-supp {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl02-clm03-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl02-clm03-style-supp {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #cccccc;
                border-bottom-color: #000000;
                padding-right: 5px;
            }

            .comm-01-style {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #fc5c5c;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                width: 800px;
                height: 40px;
                padding-right: 10px;
                border-bottom-width: 4px;
	            border-bottom-style: solid;
                border-bottom-color: #fc5c5c;
            }
            .comm-02-style {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 18px;
                color: #fc5c5c;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                width: 145px;
	            border-bottom-style: solid;
                border-bottom-color: #fc5c5c;
                border-bottom-width: 4px;
                padding-right: 10px;
            }

                .auto-style1 {
                    height: 23px;
                }


             /* Notes Section */
            .notes-01-style {
                font-family: 'Droid Sans', sans-serif;
                font-size: 13px;
                color: #000000;
                font-weight:400;
                letter-spacing: -0.05em;
            }


            /*Delivery Section */

            .tbl-delivery-01-outside {
	            height: 25px;
                width: 788px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                border-left-color: #ffffff;
            }
            .tbl-delivery-01-outside-2nd {
	            height: 25px;
                width: 788px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                border-left-color: #ffffff;
            }
            .tbl-delivery-01-inside {
	            height: 23px;
                width: 781px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                padding-left: 5px;
            }
            .tbl-delivery-02-outside {
	            height: 25px;
                width: 74px;
                text-align: right;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
            }
            .tbl-delivery-02-inside {
	            height: 23px;
                width: 65px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
            }
            .tbl-delivery-03-outside {
	            height: 25px;
                width: 69px;
                color: #cccccc;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
                padding-right: 5px;
                background-color: #eeeeee;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
            }
            .tbl-delivery-04-outside {
	            height: 25px;
                width: 788px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                border-left-color: #ffffff;
            }
            .tbl-delivery-04-inside {
	            height: 23px;
                width: 781px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #ffffff;
	            border-right-color: #ffffff;
	            border-bottom-color: #ffffff;
                border-left-color: #ffffff;
                padding-left: 5px;
                background-color: #ffffff;
            }
            .tbl-delivery-05-outside {
	            height: 25px;
                width: 69px;
                color: #cccccc;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
                padding-right: 5px;
                background-color: #eeeeee;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
            }
            .tbl-delivery-05-outside-2nd {
	            height: 25px;
                width: 69px;
                color: #cccccc;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
                padding-right: 5px;
                background-color: #eeeeee;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
            }
            .tbl-delivery-06-outside {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
            }
            .tbl-delivery-06-outside-2nd {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
            }
            .tbl-delivery-06-inside {
	            height: 23px;
                width: 65px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
            }

    /* TOTALS SECTION STYLES*/

            .tbl-totals-01 {
	            height: 30px;
                width: 778px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 10px;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
            }
            .tbl-totals-02 {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #ffffff;
                padding-right: 5px;
                background-color: #e5e4e4;
            }
            .tbl-totals-03 {
	            height: 25px;
                width: 70px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-bottom-width: 2px;
	            border-bottom-style: solid;
	            border-bottom-color: #ffffff;
                padding-right: 5px;
                background-color: #f2d3bc;
            }
            .tbl-totals-04 {
	            height: 30px;
                width: 778px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 10px;
                background-color: #eeeeee;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
            }
            .tbl-totals-05 {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #ffffff;
                padding-right: 5px;
                background-color: #eeeeee;
            }
            .tbl-totals-06 {
	            height: 25px;
                width: 69px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-bottom-width: 2px;
	            border-bottom-style: solid;
	            border-bottom-color: #ffffff;
                padding-right: 5px;
                background-color: #feddc5;
            }
            .tbl-totals-07 {
	            height: 30px;
                width: 780px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 14px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 10px;
                background-color: #eeeeee;
                border-right-width: 2px;
	            border-right-style: solid;
	            border-right-color: #fff;
            }
            .tbl-totals-08 {
	            height: 25px;
                width: 68px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #666666;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 2px;
	            border-right-style: solid;
	            border-right-color: #ffffff;
                padding-right: 5px;
                background-color: #eeeeee;
            }
            .tbl-totals-09 {
	            height: 25px;
                width: 70px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #666666;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 5px;
                background-color: #feddc5;
            }
            
    /* TABLE PROMO ITEMS*/

            .tbl04-promo00-style {
                -webkit-print-color-adjust: exact;
                width: 23px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:center;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-right-width: 1px;
                border-left-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-right-color: #ffffff;
                border-left-color: #cccccc;
                border-bottom-color: #000000;
                background:#CCCCCC;
            }
            .tbl04-promo01-style {
                -webkit-print-color-adjust: exact;
                width: 725px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:left;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-left: 5px;
            }
            .tbl04-promo02-style {
                -webkit-print-color-adjust: exact;
                width: 29px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                background:#CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl04-promo03-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align: right;
                font-weight: 400;
                letter-spacing: -0.05em;
                background: #CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl04-promo04-style {
                -webkit-print-color-adjust: exact;
                width: 69px;
                height: 35px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 11px;
                color: #000000;
                text-align: right;
                font-weight: 400;
                letter-spacing: -0.05em;
                background: #CCCCCC;
	            border-right-width: 1px;
                border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #cccccc;
                border-bottom-color: #000000;
                padding-right: 5px;
            }
            .tbl04-promo-00-outside {
	            height: 25px;
                width: 23px;
                text-align: left;
	            border-left-width: 1px;
	            border-right-width: 1px;
                border-bottom-width: 1px;
	            border-left-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-left-color: #ffffff;
	            border-right-color: #ffffff;
                border-bottom-color: #666666;
            }
            .tbl04-promo-00-outside-2nd {
	            height: 25px;
                width: 23px;
	            border-left-width: 1px;
	            border-right-width: 1px;
                border-bottom-width: 1px;
	            border-left-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-left-color: #ffffff;
	            border-right-color: #ffffff;
                border-bottom-color: #666666;
                text-align: center;
                cursor: pointer;
            }
            .tbl04-promo-01-outside {
          
	            height: 25px;
                width: 730px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
                text-align: left;
         
            }
            .tbl04-promo-01-outside-2nd {
	            height: 25px;
                width: 730px;
	            border-bottom-width: 1px;
                border-right-width: 1px;
	            border-bottom-style: solid;
                border-right-style: solid;
	            border-bottom-color: #666666;
                border-right-color: #ffffff;
                text-align: left;
            }
            .tbl04-promo-01-inside {
         
	            height: 23px;
                width: 723px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
            
                /*color: #494949;*/
                color:darkgreen;
                font-weight:400;
                font-weight:bold;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: left;
                padding-left: 5px;
          

            }
            .tbl04-promo-02-inside {
           
	            height: 23px;
                width: 25px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
                /*color: #494949;*/
                color:darkgreen;
                font-weight:400;
                font-weight:bold;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
          
            }
            .tbl04-promo-02-outside {
          
	            height: 25px;
                width: 34px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
          
            }
            .tbl04-promo-02-outside-2nd {
	            height: 25px;
                width: 34px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
            }
            .tbl04-promo-03-outside {
          
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
          
            }
            .tbl04-promo-03-outside-2nd {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
            }
            .tbl04-promo-03-inside {
           
	            height: 23px;
                width: 65px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 14px;
                /*color: #494949;*/
                color:darkgreen;
                font-weight:400;
                font-weight:bold;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
           
            }
            .tbl04-promo-04-outside {
           
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
          
            }
            .tbl04-promo-04-outside-2nd {
	            height: 25px;
                width: 74px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #ffffff;
	            border-bottom-color: #666666;
                text-align: right;
            }
            .tbl04-promo-04-inside {
	            height: 23px;
                width: 65px;
                font-family: 'Droid Sans', sans-serif;
   
                font-size: 14px;
                /*color: #494949;*/
                color:darkgreen;
                font-weight:400;
                font-weight:bold;
                letter-spacing: -0.05em;
	            border-top-width: 1px;
                border-left-width: 1px;
	            border-right-width: 1px;
	            border-bottom-width: 1px;
	            border-top-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
                border-left-style: solid;
	            border-top-color: #fff;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
                border-left-color: #fff;
                text-align: right;
                padding-right: 5px;
          
            }

    /* TOTALS SECTION LINES */
            .line-totals-01 {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-bottom-width: 2px;
	            border-bottom-style: solid;
	            border-bottom-color: #000000;
            }
            .line-totals-02 {
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 5px;
                color: #494949;
                font-weight:400;
                letter-spacing: -0.05em;
	            border-top-width: 2px;
	            border-top-style: solid;
	            border-top-color: #000000;
                height: 10px;
            }


    /* DETAILS SECTION */

            .tbl-details-01 {
	            height: 30px;
                width: 150px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #666666;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 10px;
                background-color: #eeeeee;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
            }
            .tbl-details-02 {
	            height: 30px;
                width: 150px;
                font-family: 'Droid Sans', sans-serif !important;
                font-size: 12px;
                color: #666666;
                text-align:right;
                font-weight:400;
                letter-spacing: -0.05em;
                padding-right: 10px;
                background-color: #eeeeee;
	            border-right-width: 2px;
	            border-bottom-width: 2px;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-right-color: #fff;
	            border-bottom-color: #fff;
            }

                .auto-style6 {
                    border-right: 2px solid #fff;
                    height: 29px;
                    width: 780px;
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 14px;
                    color: #000000;
                    text-align: right;
                    font-weight: 400;
                    letter-spacing: -0.05em;
                    padding-right: 10px;
                    background-color: #eeeeee;
                }
                .auto-style7 {
                    border-right: 2px solid #ffffff;
                    height: 29px;
                    width: 68px;
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 14px;
                    color: #000000;
                    text-align: right;
                    font-weight: 400;
                    letter-spacing: -0.05em;
                    padding-right: 5px;
                    background-color: #eeeeee;
                }
                .auto-style8 {
                    height: 29px;
                    width: 70px;
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 14px;
                    color: #000000;
                    text-align: right;
                    font-weight: 400;
                    letter-spacing: -0.05em;
                    padding-right: 5px;
                    background-color: #feddc5;
                }
                .ui-autocomplete-category {
                    font-weight: bold !important;
                    padding: .2em .4em;
                    margin: .8em 0 .2em;
                    line-height: 1.5;
                }
                .auto-style9 {
                    width: 670px;
                }
            .top-box-style {
	            border-left-width: 2px;
	            border-right-width: 2px;
                border-bottom-width: 2px;
	            border-left-style: solid;
	            border-right-style: solid;
	            border-bottom-style: solid;
	            border-left-color: #000000;
	            border-right-color: #000000;
                border-bottom-color: #000000;
                text-align: center;
            }
                .auto-style10 {
                    height: 10px;
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 5px;
                    width: 940px;
                }
                .auto-style11 {
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 18px;
                    color: #000000;
                    font-weight: 400;
                    letter-spacing: -1px;
                    width: 940px;
                }
                .auto-style12 {
                    font-family: 'Droid Sans', sans-serif;
                    font-size: 13px;
                    color: #000000;
                    font-weight: 400;
                    letter-spacing: -0.05em;
                    width: 940px;
                }

    /* BODY */
    body {
        margin-top: 0px;
    }

        </style>

        <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>
        <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,500,600,700,800' rel='stylesheet' type='text/css'>

        <script src="//code.jquery.com/jquery-1.10.2.js"></script>
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
        <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
        <script type="text/javascript">

            var Dialog = '';
            var Dialog_Canel = '';
            var Dialog_SuppNote = '';

            //This Method Load Supplier Note window
            function LoadSuppNote(SuppName) {
                $('#taSuppNote').val(''); //Reset the Notes Section from the Begining 
                $('#spSupplierTitle').val(SuppName);
                var TempID = $('#<%=TempID.ClientID%>').val();
                //Make the Ajax call Populate Supplier Notes if Exsists
                var SupplierNote = '';

                $.ajax({
                    type: "POST",
                    url: "Process/getSupplierNotes.aspx",
                    async: false,
                    data: {
                        suppname: SuppName,
                        id: TempID
                    },
                    success: function (msg) {
                        SupplierNote = msg;
                    },
                    error: function (xhr, err) {
                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                        alert("responseText: " + xhr.responseText);
                    },
                });

                $('#taSuppNote').val(SupplierNote);

                Dialog_SuppNote = $('#Dialog-SuppNotes').dialog({
                    resizable: false,
                    modal: true,
                    title: 'SUPPLIER-NOTES',
                    height: 400,
                    width: 710
                });

                return false;
            }
            //End Method Load Suppliert Note Window


            //Function CloseNote Dialog
            function CloseNoteDialog() {
                Dialog_SuppNote.dialog('close');
            }
            //End Mehtod CloseNote Dialog


            //This Function Initialize the Delete Dialog Window
            function CancelInvoiceDialog() {
                Dialog_Canel = $('#Dialog-DeleteConfirmation').dialog({
                    resizable: false,
                    modal: true,
                    title: 'CANCEL INVOICE CONFIRMATION',
                    height: 400,
                    width: 710
                });


                return false;
            }

            //This Function Close the Delete Dialog Window
            function CloseCancelInvoiceDialog() {
                Dialog_Canel.dialog('close');
            }

            //This Function Inititalize the Submit DialogWindow
            function SubmitDialog(Message, url, PrintScreenUrl) {
                $('#SubmitMessage').html(Message);
                //Set the Navogation URL
                $('#navigateURL').val(url);
                //Set the Print Screen URL
                $('#printscreenURL').val(PrintScreenUrl);


                Dialog = $('#Dialog-Submit-Confirmation').dialog({
                    resizable: false,
                    modal: true,
                    title: 'SUBMIT CONFIRMATION',
                    height: 400,
                    width: 710
                });

                return false;
            }


            //This Function Close the DialogWindow
            function CloseDialog() {
                //Close the Dialog
                ViewEditDialog.dialog("close");
            }

            function getParameterByName(name) {
                name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
            }

            function supplierDel() {
                var supplierDel = '';

            }

            //This Function Remove Supplier from Suppler Delivery Table and Repopulate Delivery Table
            function RemoveSupplier(suppname) {
                $('#tblSupplierDeliveryCost tr').each(function () {

                    if ($(this).find('input[name="suppdeldet"]').val() == suppname) {
                        $(this).remove();
                        //PopulateSupplierNotesTable();
                    }
                });

            }
            //End function Remove Supplier and Repopulate Delivery Table


            //This function checks wether Supplier Exsists or not
            function IsSupplierExsists(suppname) {
                var count = 0;
                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);

                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == suppname) {
                        count++;
                    }
                });

                return count;

            }
            //End Function Checkes Supplier Exsists or not


            //Modification done here Migration New Changes 16/6/2015
            function Repopulate_OLD() { //Not in Use

                var AusjetCount = 0;
                var QImagingCount = 0;
                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);
                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == 'Ausjet') {
                        AusjetCount++;
                    }
                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == 'Q-Imaging') {
                        QImagingCount++;
                    }

                });

                $('#tblSupplierDeliveryCost').html('');

                if (AusjetCount > 0) {

                    //Add a Supplier Delivery Row Here with Ausjet 
                    var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-04-inside" id="suppdeldet" value="Ausjet"/></td><td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td></tr>');

                    //$('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                }
                if (QImagingCount > 0) {
                    //Add a Supplier Delivry Row here Q-Imaging 
                    var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-04-inside" id="suppdeldet" value="Q-Imaging"/></td><td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td></tr>');
                    //$('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                }

            }


            //Modification done here Migrating new changes 16/6/2015
            function SuppDelTable_OLD() { //Not in Use

                var count = 0;
                var AusjetCount = 0;
                var QImagingCount = 0;

                //Add All the Suppliers to As an Array 

                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);

                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == 'Ausjet') {
                        AusjetCount++;
                    }
                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == 'Q-Imaging') {
                        QImagingCount++;
                    }

                });

                if (AusjetCount == 0) {

                    $('#tblSupplierDeliveryCost tr').each(function () {

                        if ($(this).find('input[name="suppdeldet"]').val() == 'Ausjet') {
                            $(this).remove();
                            //PopulateSupplierNotesTable();
                        }
                    });
                }


                if (QImagingCount == 0) {

                    $('#tblSupplierDeliveryCost tr').each(function () {

                        if ($(this).find('input[name="suppdeldet"]').val() == 'Q-Imaging') {
                            $(this).remove();
                            //PopulateSupplierNotesTable();
                        }
                    });
                }

            }

            //Populate the Supplier Notes Table according to Actions AutoPopulate ItemDesc,Add New Row,Delete a Row
            function PopulateSupplierNotesTable() {

                var Snotes = '';
                //Capture the Previous Values
                $('#tblSupplierNotes tr').each(function (i, row) {

                    var $notes_row = $(row);
                    Snotes = Snotes + $notes_row.find('#suppName').val() + ":" + $notes_row.find('#taSuppNotes').val();
                    Snotes = Snotes + "|";
                });



                $('#tblSupplierNotes').html(''); //Clear Previous Results

                //Repopulate the Table According to the SupplierDelivery Table
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                    var $row = $(row);
                    var sname = $row.find('input[name*="suppdeldet"]').val();
                    //Populate the SupplierNotes Table
                    $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes"  style="width:780px; height:100px;"  ></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');
                    $('#tblSupplierNotes').append($row);
                    $row.find('#suppName').val(sname);

                    /*Enable enter key in text areas*/


                    /*End of enabling enter key*/

                    var Items = Snotes.split('|');
                    for (i = 0; i < Items.length; i++) {
                        if (Items[i]) {
                            var Item = Items[i].split(':');
                            if ($row.find('#suppName').val() == Item[0]) {
                                $row.find('#taSuppNotes').val(Item[1]);//Supplier Notes
                            }
                        }
                    }

                });

            }
            //End Populating Supplier Notes Table

            function AddSupplierNotesRow(SuppName) {
                $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes"></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>').insertAfter(".supp-notes-row:last");
                $row.find('#suppName').val(SuppName);
            }

            $(document).ready(function () {
                window.print();
                window.onfocus = function () { window.close(); }
                //Get the Random number for the Suppliers 
                //Temp_ID=Math.random();
                //Write it to the Hidden Filed
                //$('#<%=TempID.ClientID%>').val(Temp_ID);
                $('#<%=TR_SplitCommission.ClientID%>').hide();


                //Submit Note Click Event
                $('#submitNote').click(function () {
                    //Ensure Supplier Exsists or Else Wrote to the DataBase 
                    var suppname = $('#spSupplierTitle').val();
                    var ID = $('#<%=TempID.ClientID%>').val();
                    var Note = $('#taSuppNote').val();
                    var result;
                    var Insert;
                    var Update;

                    $.ajax({
                        type: "POST",
                        url: "Process/SuppExsists.aspx",
                        async: false,
                        data: {
                            suppname: suppname,
                            id: ID
                        },
                        success: function (msg) {
                            result = msg;
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });

                    if (result == 'NO') {
                        //Make the Ajax call and Insert the Record
                        $.ajax({
                            type: "POST",
                            url: "Process/WriteTempSuppNotes.aspx",
                            async: false,
                            data: {
                                suppname: suppname,
                                id: ID,
                                Note: Note
                            },
                            success: function (msg) {
                                Insert = msg;
                                //alert(Insert);
                            },
                            error: function (xhr, err) {
                                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                alert("responseText: " + xhr.responseText);
                            },
                        });
                    }
                    else {
                        //Update the Record
                        $.ajax({
                            type: "POST",
                            url: "Process/UpdateNote.aspx",
                            async: false,
                            data: {
                                suppname: suppname,
                                id: ID,
                                Note: Note
                            },
                            success: function (msg) {
                                Update = msg;
                                //alert(Update);
                            },
                            error: function (xhr, err) {
                                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                alert("responseText: " + xhr.responseText);
                            },
                        });
                    }


                    CloseNoteDialog();

                });

                //Cancel Note Click Event
                $('#cancelNote').click(function () {
                    //Close the Dialog Window
                    CloseNoteDialog();
                });


                $('#<%=btnCancelInvoice.ClientID%>').click(function () {
                    CancelInvoiceDialog();
                });


                $('#btnYES').click(function () {
                    var OrderID = $('#<%=hdnORDERID.ClientID%>').val();
                    var ContactID = $('#<%=hdnContactID.ClientID%>').val();
                    var CompanyID = $('#<%=hdnCompanyID.ClientID%>').val();

                    if (OrderID) {
                        $.ajax({
                            type: "POST",
                            url: "process/ProcessCancelInvoice.aspx",
                            data: {
                                OrderID: OrderID,
                            },
                            success: function (msg) {
                                if (parseInt(msg) > 0) {

                                    window.location.href = "Orders/AllOrders.aspx";
                                }
                                else {
                                    alert('Error Canceling Invoice');
                                }
                            },
                            error: function (xhr, err) {
                                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                alert("responseText: " + xhr.responseText);
                            },
                        });
                    }

                });

                $('#btnNO').click(function () {
                    CloseCancelInvoiceDialog();
                });


                //Return to DashBoard Click Event
                $('#btnReturnDashBoard').click(function () {
                    window.location.href = $('#navigateURL').val();
                });

                //Print Order Click Event
                $('#btnPrintOrder').click(function () {

                    if ($('#printscreenURL').val()) {
                        window.open($('#printscreenURL').val(), '_blank', "location=no");
                    }
                });

                //PrintOrder Click Event
                $('#btnPrint').click(function () {
                    var url = "Print/PrintCreditNote.aspx?Oderid=" + $('#<%=hdnORDERID.ClientID%>').val() + "&cid=" + $('#<%=hdnContactID.ClientID%>').val() + "&Compid=" + $('#<%=hdnCompanyID.ClientID%>').val();
                    window.open(url, '_blank');
                });



                /*Disable Form Submit on Enter Key*/
                $(window).keydown(function (event) {
                    if (event.keyCode == 13) {
                        event.preventDefault();
                        return false;
                    }
                });


                /* Modification here 16/6/2015 - Code Migration*/



                function SuppDelTable() {

                    Suppliers = $(' #<%=hdnAllSuppliers.ClientID %>').val();
                    var arr_Suppliers = Suppliers.split(':');

                    //Fetch Exsisting Delivery Cost
                    var DelCost = '';
                    $('#tblSupplierDeliveryCost tr').each(function (i, row) {

                        var $row = $(row);
                        var sname = $row.find('input[name*="suppdeldet"]').val();
                        var sCost = $row.find('input[name*="suppdelcost"]').val();

                        DelCost = DelCost + sname + ":" + sCost + "|";

                    });


                    //End Fetching Delivery Cost
                    $('#tblSupplierDeliveryCost').html('');
                    var Count = 0;

                    for (k = 0; k < arr_Suppliers.length; k++) {
                        if (arr_Suppliers[k] != '') {

                            if (IsSupplierExsists(arr_Suppliers[k]) > 0) {

                                var RowDelCost = '';
                                //Preserve the Previous Delivery Cost
                                if (DelCost) {
                                    var CostItems = DelCost.split('|');
                                    for (j = 0; j < CostItems.length; j++) {
                                        if (CostItems[j] != '') {
                                            var item = CostItems[j].split(':');
                                            if (arr_Suppliers[k] == item[0]) {
                                                RowDelCost = item[1];
                                            }
                                        }
                                    }

                                }
                                //End If 
                                //
                                //Add a Supplier Row here 

                                var clientfunction = "LoadSuppNote('" + arr_Suppliers[k] + "');";

                                //Check Wether Supplier already has a note or not
                                //If has Make it Edit Note


                                //var Temp=<a href="#"  onclick="'+ clientfunction + '">[ADD A NOTE]</a>
                                var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-01-outside-2nd"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-01-inside" id="suppdeldet" value=' + arr_Suppliers[k] + '></td><td align="right" class="tbl-delivery-05-outside-2nd"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td align="right" class="tbl-delivery-06-outside-2nd"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost" value=' + RowDelCost + '></td></tr>');
                                //$('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                                $SuppDelCost_Row.find('input[name="suppdelcost"]').blur(function () {

                                    update_price();
                                });


                            }

                        }

                    }
                    //Populate the Supplier Notes Table
                    PopulateSupplierNotesTable();


                }

                function Repopulate() {
                    Suppliers = $(' #<%=hdnAllSuppliers.ClientID %>').val();
                    var arr_Suppliers = Suppliers.split(':');
                    $('#tblSupplierDeliveryCost').html('');

                    for (k = 0; k < arr_Suppliers.length; k++) {
                        if (arr_Suppliers[k] != '') {

                            if (IsSupplierExsists(arr_Suppliers[k]) > 0) {
                                //Add a Supplier Row here 
                                var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" type="text" disabled="disabled" class="tbx_cust_delivery" id="suppdeldet" value=' + arr_Suppliers[k] + '></td><td align="right" class="tbx_supp_delivery_cost_na"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true">N/A</td><td align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td></tr>');
                                //$('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                                $SuppDelCost_Row.find('input[name="suppdelcost"]').blur(function () {

                                    update_price();
                                });
                                //Popualte Supplier Notes Table
                                PopulateSupplierNotesTable();
                            }

                        }

                    }

                }

                /*End Modificatiion here -Code Migration*/

                /*Promotional Items Seetion*/
                function populateProItems(ProItems) {
                    var arr_ProItems = ProItems.split("|");

                    for (i = 0; i < arr_ProItems.length; i++) {
                        if (arr_ProItems[i]) {

                            var arr_ProItem = arr_ProItems[i].split(":");
                            if (i == 0) {
                                //Add it to the First Row
                                AddProItemFirstrow(arr_ProItem[0], arr_ProItem[1], arr_ProItem[2], arr_ProItem[3]);
                            }
                            if (i != 0) {
                                //Add it the subsequent rows
                                AddProItemRow(arr_ProItem[0], arr_ProItem[1], arr_ProItem[2], arr_ProItem[3]);
                            }
                        }
                    }

                }

                function AddProItemFirstrow(proitem, procost, quantity, shippingcost) {
                    $('#promoitem').val(proitem);
                    $('#promocost').val(procost);
                    $('#promoqty').val(quantity);
                    $('#shippingCost').val(shippingcost);
                }

                function bindPromoItems($Pro_Row) {

                    $Pro_Row.find('#promoitem').autocomplete({
                        source: "Fetch/FetchPromoItems.aspx",
                        delay: 0,
                        create: function () {
                            $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                                return $('<li>')
                                .append('<a>' + item.promodesc + '</a>')
                                .appendTo(ul);
                            }
                        },
                        select: function (event, ui) {
                            $Pro_Row.find('#promoitem').val(ui.item.promodesc);
                            $Pro_Row.find('#promocost').val(ui.item.promocost);
                            $Pro_Row.find('#promoqty').val(ui.item.qty);
                            $Pro_Row.find('#hidden_promo_item_id').val(ui.item.id);
                            $Pro_Row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                            event.preventDefault();
                        }
                    });


                    $Pro_Row.find('#promocost').blur(update_price);
                    $Pro_Row.find('#promoqty').blur(update_price);
                    $Pro_Row.find('#promoqty').blur(update_price);
                    $Pro_Row.find('#shippingCost').blur(update_price);
                }

                function AddProItemRow(proitem, procost, quantity, shippingcost) {
                    $row_proItem = $('<tr class="promo-row1"><td><a class="delete_proitem" title="Remove row"><img src="/Images/x_none.png" width="16" height="16" /></a><input type="text" name="promoitem" id="promoitem"/></td><td><input type="text" name="promocost" id="promocost"/></td><td><input type="text" name="promoqty" id="promoqty"/></td><td><input type="text" name="shippingCost" id="shippingCost" /></td><td><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" hidden="hidden"/></td></tr>').insertAfter(".promo-row1:last");
                    $row_proItem.find('#promoitem').val(proitem);
                    $row_proItem.find('#promocost').val(procost);
                    $row_proItem.find('#promoqty').val(quantity);
                    $row_proItem.find('#shippingCost').val(shippingcost);
                    bindPromoItems($row_proItem);

                }
                /*End Promotional Items Section*/

                function fillOrderItems(OrderItems, Order, SuppNotes) {



                    var arr_OrderItems = OrderItems.split("|");
                    var arr_Order = Order.split(":");

                    //Modification done 1/5/2015  SupplierNotes Population

                    var arr_suppnotes = SuppNotes.split("|");
                    for (i = 0; i < arr_suppnotes.length; i++) {
                        if (arr_suppnotes[i]) {
                            var note = arr_suppnotes[i].split(':');
                            $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes" style="width:780px; height:100px;"></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');

                            //NOT IN USE 
                                //$('#tblSupplierNotes').append($row);
                                //$row.find('#suppName').val(note[0]);
                                //$row.find('#taSuppNotes').val(note[1]);
                        }

                    }

                    //End SupplierNotes 

                    //For the First Item Fill the Table First Row
                    var arr_FirstOrderItem = arr_OrderItems[0].split(",");
                    $('#ItemDesc').val(arr_FirstOrderItem[1]);
                    $('#suppliercode').val(arr_FirstOrderItem[4]);
                    $('#COG').val(parseFloat(arr_FirstOrderItem[3]).toFixed(2));
                    $('#qty').val(arr_FirstOrderItem[5]);
                    $('#UnitPrice').val(parseFloat(arr_FirstOrderItem[2]).toFixed(2));
                    $('#hidden_item_code').val(arr_FirstOrderItem[0]);
                    $('#hidden_Supplier_Name').val(arr_FirstOrderItem[6]);
                    //MODIFIED HERE ADD SUPLLIER NAME
                    $('#ItemType').val(arr_FirstOrderItem[6]);

                    //Update the Price for the First Row
                    update_price_firstrow();

                    //For the Susquent Elements 
                    for (j = 1; j < arr_OrderItems.length; j++) {
                        if (arr_OrderItems[j]) {

                            var arr_OrderItem = arr_OrderItems[j].split(",");
                            var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x_none.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                            $row.find('#ItemDesc').val(arr_OrderItem[1]);
                            $row.find('#suppliercode').val(arr_OrderItem[4]);
                            $row.find('#COG').val(parseFloat(arr_OrderItem[3]).toFixed(2));
                            $row.find('#qty').val(arr_OrderItem[5]);
                            $row.find('#UnitPrice').val(parseFloat(arr_OrderItem[2]).toFixed(2));
                            $row.find('#hidden_item_code').val(arr_OrderItem[0]);
                            $row.find('#hidden_Supplier_Name').val(arr_OrderItem[6]);
                            //MODIFIED HERE ADD SUPPLIER NAME
                            $row.find('#ItemType').val(arr_OrderItem[6]);


                            //Update the Price for that Row 
                            update_price_row($row);
                            //Bind Row Functonalities for Austopopulations ,prince updation ,etc
                            bind($row);
                        }

                    }
                    //End populating for Susquent Elements

                    
                    var SuppDelCostItems = arr_Order[0];
                    //var ProItems = arr_Order[1];
                    var CusDelCostItems = arr_Order[1];

                    //Set Customer DelCost Items

                    var arr_CusDelCost = arr_Order[1].split("|");

                    //Customer Delivery Cost 
                        //$('#deldet').val(arr_CusDelCost[0]);
                        //$('#delcost').val(arr_CusDelCost[1]);

                    //Set Promotional Cost Items

                    //var arr_ProitemCost = ProItems.split("|");

                    //Populate Promotional Items  
                       // if ($('#<%=hdnEditproitpems.ClientID %>').val()) {
                       // populateProItems($('#<%=hdnEditproitpems.ClientID %>').val());
                       //}

                    //Supplier Delivery Cost Population
                    arr_SuppDelCost = SuppDelCostItems.split("|");
                    //For the Fist Row
                    var arr_SuppDelCost_FirstRow = arr_SuppDelCost[0].split(",");

                        //$('#suppdeldet').val(arr_SuppDelCost_FirstRow[0]);
                        //$('#suppdelcost').val(arr_SuppDelCost_FirstRow[1]);

                    //For SubSquent SuppDelCost Rows 
                    for (k = 1; k < arr_SuppDelCost.length; k++) {
                        if (arr_SuppDelCost[k]) {

                            var arr_SuppDelCost_Row = arr_SuppDelCost[k].split(',');
                            var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" disabled="disabled" type="text" class="tbx_cust_delivery" id="suppdeldet"></td><td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td><td width="76" align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td><td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td><td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td></tr>').insertAfter(".supp-del-row:last");
                            $SuppDelCost_Row.find('#suppdeldet').val(arr_SuppDelCost_Row[1]);
                            $SuppDelCost_Row.find('#suppdelcost').val(arr_SuppDelCost_Row[2]);

                        }
                    }
                }

                //Edit Functionalities
                if ($('#<%=hdnEditOrderItems.ClientID%>').val() != '') {


                    //Display the Print Button
                    $('#btnPrint').show();

                    fillOrderItems($('#<%=hdnEditOrderItems.ClientID%>').val(), $('#<%=hdnEditOrder.ClientID%>').val(), $('#<%=hdnEditSupplietNotes.ClientID%>').val());
                    update_price();

                }
                //End Edit functionalities

                $('#<%=btnOrderSubmit.ClientID%>').click(function () {

                    var myData = [];
                    var ErrField = '';
                    var OrderItems = '';

                    $('#tblLineItems tr').each(function (i, row) {

                        var $row = $(row);
                        myData.push($row.find('input[name*="hidden_item_code"]').val());
                        myData.push($row.find('input[name*="ItemDesc"]').val());
                        myData.push($row.find('input[name*="suppliercode"]').val());
                        myData.push($row.find('input[name*="COG"]').val());
                        myData.push($row.find('input[name*="qty"]').val());
                        myData.push($row.find('input[name*="UnitPrice"]').val());
                        myData.push($row.find('input[name*="hidden_Supplier_Name"]').val());
                        myData.push("|");

                    });

                    //Populate the OrderItems 

                    for (i = 0; i < myData.length; i++) {
                        OrderItems = OrderItems + myData[i];
                    }

                    $('#<%=OrderItems.ClientID%>').val(myData);

                    //End Writing the Order Items

                    //Total Profit
                    $('#<%=hdnProfit.ClientID %>').val($('#totalprofit').html().replace("$", ""));

                    //Profit Total  for the Customer Invoice
                    $('#<%=hdnSubTotal.ClientID %>').val($('#ProfitExGST').html().replace("$", ""));
                    $('#<%=hdnTotal.ClientID %>').val($('#profitFullTotal').html().replace("$", ""));

                    //COG Total for the Supplier Invoice
                    $('#<%=hdnCOGSubTotal.ClientID %>').val($('#subtotal').html().replace("$", ""));
                    $('#<%=hdnCOGTotal.ClientID %>').val($('#fulltotal').html().replace("$", ""));


                    //Supplier Delivery Cost Items 
                    var SuppDelItems = [];
                    $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                        var $row = $(row);

                        //SuppDelItems.push($row.find('input[name*="suppdeldet"]').val());
                        //SuppDelItems.push($row.find('input[name*="suppdelcost"]').val());
                        //SuppDelItems.push("|");
                    });

                    $('#<%=hdnSupplierDelCostItems.ClientID%>').val(SuppDelItems);


                    //Promotional Items
                    $('#<%=hdnProItems.ClientID%>').val($('#promoitem').val() + "|" + $('#promocost').val() + "|" + $('#promoqty').val());

                    //Cmustomer Delivery Cost Items

                    $('#<%=CusDelCostItems.ClientID%>').val($('#deldet').val() + "|" + $('#delcost').val());


                    //Supplier Notes Section
                    var SuppNotes = [];
                    $('#tblSupplierNotes tr').each(function (i, row) {

                        var $row_Notes = $(row);
                        SuppNotes.push($row_Notes.find('input[name*="suppName"]').val());
                        SuppNotes.push($row_Notes.find('#taSuppNotes').val());
                        SuppNotes.push("|");
                    });

                    $('#<%=hdnSupplierNotes.ClientID%>').val(SuppNotes);
                    //End Supplier Notes Section


                    function getProItemCount() {
                        flag = false;
                        var Count = 0;
                        $('#tblProItems tr').each(function (i, row) {
                            var $row_ProItems = $(row);
                            Count++;
                        });

                        if (($('#promoitem').val() == '') && (Count == 1)) {
                            flag = true; //No Promotional Items
                        }

                        return flag;

                    }

                    //Promotional Items Section
                    var ProItems = [];
                    $('#tblProItems tr').each(function (i, row) {

                        var $row_ProItems = $(row);

                        ProItems.push($row_ProItems.find('#promoitem').val());
                        ProItems.push($row_ProItems.find('#promocost').val());
                        ProItems.push($row_ProItems.find('#promoqty').val());
                        ProItems.push($row_ProItems.find('#shippingCost').val());

                        ProItems.push('|');
                    });
                    //End Promotional Items Section

                    if (!getProItemCount()) {
                        $('#<%=hdnPromotionalItems.ClientID%>').val(ProItems);
                }

                });

                //Add new Promotional Item
                $('#addnewProItem').click(function () {

                    //If not First Item is Empty
                    if (!($('#promoitem').val() == '')) {
                        var $row = $('<tr class="promo-row1"><td class="tbl04-promo-00-outside-2nd"><a class="delete_proitem" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl04-promo-01-outside-2nd"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/></td><td class="tbl04-promo-02-outside-2nd"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td><td class="tbl04-promo-03-outside-2nd"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  <td class="tbl04-promo-04-outside-2nd"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td></tr>').insertAfter(".promo-row1:last");
                        bindPromotionalItemFunctions($row);
                    }
                    else {
                        alert('FIRST PROMOTIONAL ITEM  MUST CONTAIN A VALUE');
                    }

                });

                //End Add new promotional Item


                //Delete promotional Row
                $(document).on('click', '.delete_proitem', function () {

                    var $tr = $(this).closest('.promo-row1');
                    $tr.remove();
                    update_total();

                });
                //End Delete promotional Row


                //This function binds the AutoPopulation functions to the proItem Row
                function bindProItem($row) {

                    $row.find('#promoitem').autocomplete(function () {
                    });
                }
                //End function bind promotional item row


                $('#addnewitem').click(function () {
                    //Validation Check wether First Item has a value if yes then add 

                    if (!($('#ItemDesc').val() == '')) {
                        var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                        bind($row);
                    }
                    else {
                        alert('FIRST LINE ITEM MUST CONTAIN A VALUE');
                    }
                });

                //Delete the Row
                $(document).on('click', '.delete', function () {
                    var $tr = $(this).closest('.item-row');

                    var name = $tr.find('input[name="hidden_Supplier_Name"]').val();

                    if (CheckSupplierNameAssociation(name) > 1) {

                    }
                    else {
                        $('#tblSupplierDeliveryCost tr').each(function () {

                            if ($(this).find('input[name="suppdeldet"]').val() == name) {
                                $(this).remove();
                                //PopulateSupplierNotesTable();
                            }
                        });

                    }

                    $tr.remove();
                    update_total();

                });
                //End Delete row

                //This function checks SupplierName Associates with Other Rows
                function CheckSupplierNameAssociation(name) {
                    var count = 0;
                    $('#tblLineItems tr').each(function () {

                        if ($(this).find('input[name="hidden_Supplier_Name"]').val() == name) {
                            count++;
                        }
                    });

                    return count;
                }
                //End Function SupplierNameAssociation


                //Check wether Supplier Exsists or not
                function findRowExsists(name) {

                    flag = false;
                    $('#tblSupplierDeliveryCost tr').each(function () {

                        if ($(this).find('input[name="suppdeldet"]').val() == name) {
                            flag = true;
                        }
                    });
                    return flag;
                }
                //End Function Supplier Exsists or not


                /*Promotional Items Functions*/

                function bindPromotionalItemFunctions($Pro_Row) {


                    $Pro_Row.find('#promoitem').autocomplete({
                        source: "Fetch/FetchPromoItems.aspx",
                        delay: 0,
                        create: function () {
                            $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                                return $('<li>')
                                .append('<a>' + item.promodesc + '</a>')
                                .appendTo(ul);
                            }
                        },
                        select: function (event, ui) {
                            $Pro_Row.find('#promoitem').val(ui.item.promodesc);
                            $Pro_Row.find('#promocost').val(ui.item.promocost);
                            $Pro_Row.find('#promoqty').val(ui.item.qty);
                            //Shipping Cost
                            $Pro_Row.find('#hidden_promo_item_id').val(ui.item.id);
                            $Pro_Row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                            $Pro.Row.find('#ItemType').val(ui.item.OriComp);
                            event.preventDefault();
                        }
                    });
                    //Blur functions 

                    $Pro_Row.find('#promoitem').blur(update_price);
                    $Pro_Row.find('#promocost').blur(update_price);
                    $Pro_Row.find('#promoqty').blur(update_price);
                    $Pro_Row.find('#shippingCost').blur(update_price);
                    //Shipping Cost blur functions 
                }
                /*End Promotional Items*/

                //This function Bind the AutoCompletion and Updtate Total Cells
                function bind($row) {
                    //Define the SupplierCost Row here 
                    $row.find('input[name="ItemDesc"]').blur(function () {
                        update_price();
                        SuppDelTable();
                        //Update the Total and COG Total 30/04/2015
                        update_price_row($row);
                    });

                    //Trigger calculations for subsequent rows
                    $row.find('input[name="suppliercode"]').blur(update_price);
                    $row.find('input[name="qty"]').blur(update_price);
                    $row.find('input[name="UnitPrice"]').blur(update_price);
                    $row.find('input[name="ItemDesc"]').blur(update_price);
                    //Binding Auto Completion for the Row

                    $.widget("custom.catcomplete", $.ui.autocomplete, {
                        _create: function () {
                            this._super();
                            this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
                        },
                        _renderMenu: function (ul, items) {
                            var that = this,
                                currentCategory = "";
                            $.each(items, function (index, item) {
                                var li;
                                if (item.SupplierName != currentCategory) {
                                    ul.append("<li class='ui-autocomplete-category'>" + item.SupplierName + "</li>")
                                    currentCategory = item.SupplierName;
                                }
                                li = that._renderItemData(ul, item);
                                if (item.SupplierName) {
                                    li.attr("aria-label", item.SupplierName + " : " + item.Description);
                                }
                            });
                        },

                        _renderItem: function (ul, item) {
                            return $("<li>")
                            .addClass(item.SupplierName)
                            .attr("data-value", item.ItemID)
                            .append($("<a>").text(item.Description + " - " + item.OEMCode))
                            .appendTo(ul);
                        }
                    });

                    $row.find('#ItemDesc').catcomplete({
                        source: "Fetch/FetchItembyInput.aspx",
                        delay: 0,
                        focus: function (event, ui) {
                            $row.find('#COG').val(ui.item.COG);
                            $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                            $row.find('#ItemType').val(ui.item.OriComp);
                            return false;
                        },
                        select: function (event, ui) {
                            $row.find('#ItemDesc').val(ui.item.Description);
                            $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                            $row.find('#hidden_item_code').val(ui.item.ItemID);
                            //Set the Hidden supplier Name for the Row
                            $('#hdn_Supp_Name').val(ui.item.SupplierName);
                            $row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                            $row.find('#OriComp').val(ui.item.OriComp);


                            if (!(findRowExsists(ui.item.SupplierName))) {
                                SuppDelTable();
                            }

                            $row.find('#COG').val(ui.item.COG);
                            $row.find('#UnitPrice').val(ui.item.ManagerUnitPrice);
                            $row.find('#qty').val(1);
                            return false;
                        }

                    });

                }
                //End Function Bind AutoCompletion and Uppate Cells


                //Supplier Delivery Cost Bind function 
                function BindSupplierDelivery($row) {
                    $row.find('input[name="suppdelcost"]').blur(update_price);
                }
                //End Suypplier Delivery Cost Bind function here


                //Supplier
                $('#suppdeldet').autocomplete({
                    source: "Fetch/FetchSupplierFees.aspx",
                    delay: 0,
                    select: function (event, ui) {
                        $('#suppdelcost').val(ui.item.StandardDeliveryCost);

                    },
                });

                //Promotion Items AutoComplete
                $('#promoitem').autocomplete({
                    source: "Fetch/FetchPromoItems.aspx",
                    delay: 0,
                    create: function () {
                        $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                            return $('<li>')
                            .append('<a>' + item.promodesc + '</a>')
                            .appendTo(ul);
                        }
                    },
                    select: function (event, ui) {
                        $('#promoitem').val(ui.item.promodesc);
                        $('#promocost').val(parseFloat(ui.item.promocost).toFixed(2));
                        $('#promoqty').val(ui.item.qty);
                        $('#hidden_promo_item_id').val(ui.item.id);
                        $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        event.preventDefault();
                    }
                });

                //Delivery Cost AutoComplete
                $('#deldet').autocomplete({
                    source: "Fetch/FetchDeliveryFees.aspx",
                    delay: 0,
                    create: function () {
                        $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                            return $('<li>')
                            .append('<a>' + item.deliverydetails + '</a>')
                            .appendTo(ul);
                        }
                    },
                    select: function (event, ui) {
                        $('#deldet').val(ui.item.deliverydetails);
                        $('#hidden_delivery_item_id').val(ui.item.id);
                        $('#delcost').val(ui.item.deliverycost);
                        event.preventDefault();
                    }
                });

                $.widget("custom.catcomplete", $.ui.autocomplete, {
                    _create: function () {
                        this._super();
                        this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
                    },
                    _renderMenu: function (ul, items) {
                        var that = this,
                            currentCategory = "";
                        $.each(items, function (index, item) {
                            var li;
                            if (item.SupplierName != currentCategory) {
                                ul.append("<li class='ui-autocomplete-category'>" + item.SupplierName + "</li>")
                                currentCategory = item.SupplierName;
                            }
                            li = that._renderItemData(ul, item);
                            if (item.SupplierName) {
                                li.attr("aria-label", item.SupplierName + " : " + item.Description);
                            }
                        });
                    },

                    _renderItem: function (ul, item) {
                        return $("<li>")
                        .addClass(item.SupplierName)
                        .attr("data-value", item.ItemID)
                        .append($("<a>").text(item.Description + " - " + item.OEMCode))
                        .appendTo(ul);
                    }
                });

                //AutoCompletion for the First Item
                $('#ItemDesc').catcomplete({
                    source: "Fetch/FetchItembyInput.aspx",
                    delay: 0,
                    focus: function (event, ui) {
                        $('#COG').val(ui.item.COG);
                        $('#suppliercode').val(ui.item.SupplierItemCode);
                        $('#ItemType').val(ui.item.OriComp);
                        return false;
                    },
                    select: function (event, ui) {
                        $('#ItemDesc').val(ui.item.Description);
                        $('#suppliercode').val(ui.item.SupplierItemCode);
                        $('#hidden_item_code').val(ui.item.ItemID);

                        $('#hdn_Supp_Name').val(ui.item.SupplierName); //Test
                        $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        $('#COG').val(ui.item.COG);
                        $('#UnitPrice').val(ui.item.ManagerUnitPrice);
                        $('#qty').val(1);
                        $('#ItemType').val(ui.item.OriComp);
                        PopulateSupplierNotesTable();
                        return false;
                    }

                });
                //End Auto Completion Find Item

                //Trigger the Supplier Name in the SupplierDeliveryCost for the first time

                $('#ItemDesc').blur(function () {

                    SuppDelTable(); //MODIFIED HERE 16/6/2015 Sumudu Kodikara 
                });

                //Trigger calculation for the first line item
                //$('#ItemDesc').blur(update_price);
                $('#suppliercode').blur(update_price);
                $('#qty').blur(update_price);
                $('#UnitPrice').blur(update_price);
                $('#ItemDesc').blur(update_price);

                //Supplier Delivry Cost
                $('#suppdelcost').blur(update_price);

                //Customer Delivery Cost update price 
                $('#deldet').blur(update_price);
                $('#delcost').blur(update_price);

                //Promotional Items First Row functionalities 

                $('#promoitem').blur(update_price);
                $('#promoqty').blur(update_price);
                $('#promocost').blur(update_price);
                $('#shippingCost').blur(update_price);

                //This Function Update price and Total for the First Row
                function update_price_firstrow() {
                    var price = ($('#qty').val()) * ($('#UnitPrice').val());
                    $('.total').html("$" + parseFloat(price).toFixed(2));
                    var cogprice = ($('#qty').val()) * ($('#COG').val());
                    $('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                    update_total();

                }
                //End  Function Update price and Total for the First row


                //This function Update price and Total for row Given by row
                function update_price_row($row) {
                    var newrow = $row;
                    var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                    newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                    var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                    newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                    update_total();
                };

                //End Function Update price and Total for row given by row 


                // Upate the prince of the Row and Update the Total
                function update_price() {


                    var newrow = $(this).parents('.item-row');
                    var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                    newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                    var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                    newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                    update_total();
                };

                ///Calculate Display Credit Note Total
                function update_total() {
                    var subtotal = 0;
                    var lineprofittotal = 0;

                    //Profit Item Total Calculation
                    $('.total').each(function (i) {
                        linesubtotal = $(this).html().replace("$", "");
                        if (!isNaN(subtotal)) subtotal += Number(linesubtotal);
                    });

                    //COG Item Total Calculation
                    $('.cogtotal').each(function (o) {
                        coglinesubtotal = $(this).html().replace("$", "");
                        if (!isNaN(lineprofittotal)) lineprofittotal += Number(coglinesubtotal);
                    });

                    //GST for the Profit Total
                    gst = (parseFloat(subtotal) * 10) / 100;

                    //GST for the COG Total
                    gst_COG = (parseFloat(lineprofittotal) * 10) / 100;

                    //COG Full Total 
                    fulltotal_COG = parseFloat(lineprofittotal) + parseFloat(gst_COG);

                    //Add Supplier Delivery Cost here and Promotional Item here
                    var SupplierDeliverCost = 0;
                    $('#tblSupplierDeliveryCost tr').each(function () {

                        //if (!isNaN(SupplierDeliverCost)) SupplierDeliverCost += Number($(this).find('input[name="suppdelcost"]').val());
                    });

                    //Supplier Del Cost without GST

                    SuppDelCostExGST = parseFloat(SupplierDeliverCost).toFixed(2);

                    //Customer Delivery Cost without GST
                    CusDelCostEXGST = parseFloat((Number($('#delcost').val()))).toFixed(2);

                    //Modified here Add promotionl item Cost
                    var promoItemCost = 0;
                    $('#tblProItems tr').each(function () {

                        if (!isNaN(promoItemCost)) {

                            promoItemCost = promoItemCost + parseFloat(Number($(this).find('#promocost').val()) * Number($(this).find('#promoqty').val())) + parseFloat(Number($(this).find('#shippingCost').val()));
                        }

                    });

                    ProItemCost = (parseFloat(promoItemCost) / 1.1);
                    //End Calculating Promotional item Cost

                    //Profit Sub Total ExGST

                    var ProfitSubTotalExGST = ((parseFloat(Number(subtotal) + Number(CusDelCostEXGST)).toFixed(2)) / 1.1);
                    //alert(ProfitSubTotalEXGST);

                    //COG SubTotal EXGST

                    var COGSubTotalExGST = parseFloat(Number(lineprofittotal) + Number(SuppDelCostExGST) + Number(ProItemCost)).toFixed(2);

                    //Commission EXGST 
                    exgsttotal = parseFloat(ProfitSubTotalExGST - COGSubTotalExGST).toFixed(2);

                    //GST Calcualtions
                    var ProfitTotalGSTAmount = parseFloat(ProfitSubTotalExGST * 0.1).toFixed(2);
                    var COGTotalGSTAmount = parseFloat(COGSubTotalExGST * 0.1).toFixed(2);

                    //FullTotal INC GST

                    TotalProfitINCGST = Number(ProfitSubTotalExGST) + Number(ProfitTotalGSTAmount);
                    TotalCOGINCGST = Number(COGSubTotalExGST) + Number(COGTotalGSTAmount);

                    //Profit Calculation
                    TOTAL_Profit = parseFloat(TotalProfitINCGST - TotalCOGINCGST).toFixed(2);

                    //Add Delivery Cost to the COG Total
                    lineprofittotal = parseFloat(lineprofittotal) + parseFloat(SupplierDeliverCost);

                    //Add Promotional Item Cost here If any
                    promotionalItemCost = parseFloat(Number($('#promocost').val()) * Number($('#promoqty').val()));

                    lineprofittotal = parseFloat(lineprofittotal) + parseFloat(promotionalItemCost);

                    //Profit Full Total
                    fulltotal = parseFloat(subtotal) + parseFloat(gst);

                    //Add Customer Delivery Cost
                    customerDeliveryCost = Number($('#delcost').val());
                    subtotal = parseFloat(subtotal) + parseFloat(Number($('#delcost').val()));

                    //Set the Hidden Field values 
                    $('#<%=hdnSupplierDeliveryCost.ClientID %>').val(SupplierDeliverCost);
                    $('#<%=hdnCustomerDeliveryCost.ClientID %>').val(customerDeliveryCost);
                    $('#<%=hdnProCost.ClientID %>').val(promotionalItemCost);
                    //End Setting the Hidden values

                    //Profit Total Display

                    $('#ProfitExGST').html("$" + parseFloat(ProfitSubTotalExGST).toFixed(2));
                    $('#profitGST').html("$" + parseFloat(ProfitTotalGSTAmount).toFixed(2));
                    $('#profitFullTotal').html("$" + parseFloat(TotalProfitINCGST).toFixed(2));

                    //Cost of Good Total Display

                    $('#subtotal').html("$" + parseFloat(COGSubTotalExGST).toFixed(2));
                    $('#gst').html("$" + parseFloat(COGTotalGSTAmount).toFixed(2));//GST COG 
                    $('#fulltotal').html("$" + parseFloat(TotalCOGINCGST).toFixed(2));


                    //// Commission Recalculation based on Ex GST values
                    //Commission_Deduction = ((parseFloat(TotalProfitINCGST).toFixed(2) - parseFloat(TotalCOGINCGST).toFixed(2)) * 0.4).toFixed(2);

                    thedeltotal = $('#deltotal').html().replace("$", "");

                    ordertotal = parseFloat(thedeltotal) + fulltotal;//Oreder COG Total
                    $('#ordertotal').html("$" + parseFloat(ordertotal).toFixed(2));//Ordet COG Total

                    //var Commission = (parseFloat(exgsttotal) * 0.4).toFixed(2);
                    // Commission = (Commission_Deduction);

                    //Find Rep Commission
                    var repCommission = 0;

                    if ($('#<%=hdnCommishSplit.ClientID%>').val() == "0") {

                        $('#TR_SalespersonCommission').hide();
                        $.ajax({
                            url: '../fetch/getoperatorcommission.aspx',
                            async: false,
                            data: {
                                repid: $('#<%=ACCOUNT_OWNER_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;

                            var CommishPerc = repCommission / 100;
                            var Commission = (parseFloat(exgsttotal) * CommishPerc).toFixed(2);

                          //  var CreditNoteID = $('#<%=hdnCreditNoteID.ClientID%>').val();
                           // if (CreditNoteID == 1653)
                             //   Commission = 16.37;

                            $('#totalprofit').html("$" + parseFloat(Commission).toFixed(2))//Comission   
                            $('#<%=hdnProfit.ClientID %>').val(TOTAL_Profit);
                            $('#<%=hdnCommision.ClientID%>').val(parseFloat(Commission).toFixed(2));
                            $('#<%=ACCOUNT_OWNER_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                        },

                        error: function (message) {
                            alert('Unable to retrieve commission for logged user. Please contact your administartor');
                        }
                    });
                }
                else {
                        $('#TR_SplitCommission').show();
                        //Split profits into two
                    var SplitProfits = exgsttotal / 2;
                    var SplitVolume = parseFloat(ProfitSubTotalExGST).toFixed(2) / 2;
                    $('#<%=VOLUME_SPLIT_AMOUNT.ClientID%>').val(SplitVolume);

                        //Split profits into two
                   var SplitProfits = exgsttotal / 2;

                        //Get Commission For Account Owner
                   $.ajax({
                       url: '../fetch/getoperatorcommission.aspx',
                       async: false,
                       data: {
                           repid: $('#<%=ACCOUNT_OWNER_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;

                            var CommishPerc = repCommission / 100;
                            var Commission = (parseFloat(SplitProfits) * CommishPerc).toFixed(2);
                            $('#CD-accountownername').html($('#<%=ACCOUNT_OWNER.ClientID%>').val().toUpperCase() + " - ");
                            $('#totalprofit').html("$" + parseFloat(Commission).toFixed(2))//Comission  
                            $('#<%=ACCOUNT_OWNER_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                        },
                        error: function (message) {
                            alert('Unable to retrieve commission for logged user. Please contact your administartor');
                        }
                    });

                        //Get Commission For Salesperon
                $.ajax({
                    url: '../fetch/getoperatorcommission.aspx',
                    async: false,
                    data: {
                        repid: $('#<%=SALESPERSON_ID.ClientID%>').val(),
                    },
                    success: function (data) {
                        repCommission = data;

                        var CommishPerc = repCommission / 100;
                        var Commission = (parseFloat(SplitProfits) * CommishPerc).toFixed(2);
                        $('#CD-salespersonname').html($('#<%=SALESPERSON_TXT.ClientID%>').val().toUpperCase() + " - ");
                        $('#totalprofitsalesperson').html("$" + parseFloat(Commission).toFixed(2))//Comission 
                        $('#<%=SALESPERSON_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                    },
                    error: function (message) {
                        alert('Unable to retrieve commission for logged user. Please contact your administartor');
                    }
                });
            }
                    //$('#<%=hdnCommision.ClientID%>').val(parseFloat(CommisonDeduction).toFixed(2));

                };
            });
            //End Calculating Order Total

        </script>
            </head>

    <body>
        <form id="form1" runat="server">

    
        <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">

            <tr>
                <td class="white-bg-style" height="50px">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    
                        <tr>
                            <td>

       <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
            <tr>
                <td>

                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
            <tr>
                <td class="auto-style1" style="display:none;"><asp:DropDownList ID="ddlNQ" runat="server">
                        <asp:ListItem Selected="False" Value="1">NEW ORDER</asp:ListItem>
                        <asp:ListItem Value="2">QUOTE</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>

            <tr>
                <td style="display:none;">&nbsp;</td>
                <!-- <td id="QorN" runat="server">Hello Hello</td> COMMENTED BY SHEHAN 31-08-2015-->
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td id="OrderTitle" runat="server" class="Title_Style"  style="display:none;">&nbsp;</td>
                            <td id="ordertd" runat="server" class="Title_Style"  ></td>
                           <%-- <td class="acc-owner-style"><div id="AccountOwnerDIV" runat="server"></div></td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td width="620px">
                                <table align="center" cellpadding="0" cellspacing="0" class="auto-style9">
                                    <tr>
                                        <td>
                                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style9">
                                                <tr>
                                                    <td height="30px" class="company-name-style"><div id="CompanyNameDIV" runat="server"></div></td>

                                                </tr>
                                                <tr>
                                                    <td class="height-10px-style">&nbsp;</td>

                                                </tr>
                                                <tr >
                                                    <td class="company-details-style"><div id="ContactInfo" runat="server"></div></td>

                                                </tr>
                                                <tr >
                                                    <td class="company-details-style"><div id="StreetAddressLine1" runat="server"></div></td>

                                                </tr>
                                                <tr >
                                                    <td class="company-details-style"><div id="StreetAddressLine2" runat="server"></div></td>

                                                </tr>
                                                <tr>
                                                    <td class="height-10px-style">&nbsp;</td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="company-details-style"><div id="ContactandEmail" runat="server"></div></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="20px">&nbsp;</td>
                            <td width="250px" style="vertical-align: top;">
                                <table align="center" cellpadding="0" cellspacing="0" class="width-250-style">
                                    <tr>
                                        <td class="delivery-add-heading-style">&nbsp;</td>
                                    </tr>
                                    <tr  style="display:none;">
                                        <td class="top-box-style" height="100px"><div id="docstampdiv" runat="server"></div></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td><div id="divOrderCreated" runat="server"></div></td>
            </tr>
            <tr style="display:none;">
                <td><div id="DeliveryCompany" runat="server"></div></td>
            </tr>

            </table>
                </td>
            </tr>
        </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                    <tr>
                                        <td class="top-tbl01-style">CREATED DATE</td>
                                        <td class="top-tbl02-style">CREATED BY</td>
                                        <td class="top-tbl02-style">CREDIT NOTE REASON</td>
                                        <td class="top-tbl02-style">STATUS</td>
                                    </tr>
                                    <tr>
                                         <td class="top-tbl03-style">
                                            <div id="divOrderCreatedDate" runat="server"></div>
                                         </td>
                                        <td class="top-tbl04-style">
                                            <div id="createdby" runat="server"></div>
                                        </td>
                                        <td class="top-tbl04-style">
                                            <div id="creditnotereason" runat="server"></div>
                                        </td>

                                         <td class="top-tbl04-style">
                                            <div id="creditStatus" runat="server"></div>
                                        </td>

               
                                      


                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="all-subheadings-style">Products</td>
                        </tr>
                        <tr>
                            <td class="height-10px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                    <tr>
                                        <td class="tbl01-clm01-style">
                                            <br />
                                            </td>
                                        <td class="tbl01-clm02-style">
                                            <br />
                                            ITEM DESCRIPTION</td>
                                        <td class="tbl01-clm03-style">
                                            <br />
                                            SUPPLIER NAME</td>
                                        <td class="tbl01-clm04-style">SUPPLIER<br />
                                            CODE</td>
                                        <td class="tbl01-clm05-style">COG
                                            <br />
                                            EX GST</td>
                                        <td class="tbl01-clm06-style">
                                            <br />
                                            QTY&nbsp;</td>
                                        <td class="tbl01-clm07-style">UNIT PRICE<br />
                                            INC GST</td>
                                        <td class="tbl01-clm08-style">TOTAL<br />
                                            INC GST</td>
                                        <td class="tbl01-clm09-style">COG TOTAL<br />
                                            EX GST</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table id="tblLineItems" width="940" border="0" align="center" cellpadding="0" cellspacing="0" id="lineitems">
                <tr class="item-row">
                  <td class="tbl-auto-row-01">&nbsp;</td>
                  <td class="tbl-auto-row-02"><label for="ItemDesc"></label>
                    <input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td>

                  <td class="tbl-auto-row-03"><label for="ItemType">
                    <input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td>
                  <td class="tbl-auto-row-04"><label for="suppliercode"></label>
                    <input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td>
                  <td class="tbl-auto-row-05"><label for="COG"></label>
                    <input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td>
                  <td class="tbl-auto-row-06"><label for="qty"></label>
                    <input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td>
                  <td class="tbl-auto-row-07"><label for="UnitPrice"></label>
                    <input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td>
                  <td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td>
                  <td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span>
                    <label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/>
                      <input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" />
                  </td>
                </tr>
              </table>
                                                        </td>
                        </tr>
                        <tr style="display:none;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td >

                                <input name="addnewitem" type="button" class="add-btn" id="addnewitem" value="ADD NEW ITEM"/>

                            </td>
                        </tr>
                        <tr>
                            <td height="20px" style="display:none;">&nbsp;</td>
                        </tr>

                        </table>
                </td>
            </tr>
            <tr>
                <td height="20px">&nbsp;</td>
            </tr>
            <tr>
                <td class="white-bg-style">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr style="display:none;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="all-subheadings-style">Delivery</td>
                        </tr>
                        <tr>
                            <td class="height-10px-style">&nbsp;</td>
                        </tr>
                        <tr >
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                    <tr>
                                        <td class="tbl02-clm01-style">
                                            <br />
                                            CUSTOMER DELIVERY TYPE&nbsp;</td>
                                        <td class="tbl02-clm02-style">COST<br />
                                            INC GST</td>
                                        <td class="tbl02-clm03-style">&nbsp;</td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>

                            
            <table width="940" border="0" align="center" cellpadding="0" cellspacing="0" >
                <tr class="del-row">
              
                    <td class="tbl-delivery-01-outside">
                        <input name="deldet" type="text" class="tbl-delivery-01-inside" id="deldet"/>
                    </td>
                    <td class="tbl-delivery-02-outside">
                        <input name="delcost" type="text" class="tbl-delivery-02-inside" id="delcost"/>
                    </td>
                    <td class="tbl-delivery-03-outside">
                        <input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden"/>
                    </td>
                </tr>
            </table>

                            </td>
                        </tr>
                        <tr>
                            <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr >
                            <td>

                                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                    <tr>
                                        <td class="tbl02-clm01-style-supp">
                                            <br />
                                            DELIVERY FROM SUPPLIER&nbsp;</td>
                                        <td class="tbl02-clm02-style-supp">&nbsp;</td>
                                        <td class="tbl02-clm03-style-supp">COST<br />
                                            EX GST</td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                        <tr>
                            <td>
                                <!--
                                <table id="tblSupplierDeliveryCost" width="940" border="0" align="center" cellpadding="0" cellspacing="0" >
                                  <tr class="supp-del-row">
                                    <td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="suppdeldet"  disabled="disabled"></td>
                                    <td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden"/><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" size="1" hidden="hidden" />&nbsp;</td>
                                    <td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"/></td>
                                    </tr>-->
    <!-- <tr class="supp-del-row"><td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="Text1"  disabled="disabled"></td><td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="Text2" size="1" hidden="hidden"/><input name="hdn_Supp_Name" type="text" id="Text3" size="1" hidden="hidden" />N/A</td><td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="Text4"/></td>
                    </tr> -->
                             <!-- </table> -->
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <tr class="item-supprow">
                    <td>
                        <table id="tblSupplierNotes">

                        </table>
                    </td>
                 </tr>

                            </td>
                        </tr>
                        <tr>
                            <td height="20px" style="display:none;">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="20px">&nbsp;</td>
            </tr>
            <tr style="display:none;">
                <td class="white-bg-style">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr style="display:none;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td class="all-subheadings-style">Promotional</td>
                        </tr>
                        <tr>
                            <td class="height-10px-style">&nbsp;</td>
                        </tr>
                    
                        <tr>
                            <td>

                                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                    <tr>
                                        <td class="tbl04-promo00-style">
                                            <br />
                                            &nbsp;</td>
                                        <td class="tbl04-promo01-style">
                                            <br />
                                            ITEM DESCRIPTION&nbsp;</td>
                                        <td class="tbl04-promo02-style">
                                            <br />
                                            QTY&nbsp;</td>
                                        <td class="tbl04-promo03-style">SHIPPING<br />
                                            INC GST</td>
                                        <td class="tbl04-promo04-style">UNIT PRICE<br />
                                            INC GST</td>
                                    </tr>
                                </table>


                            </td>
                        </tr>
                            <tr class="Item_ProRow">
                            <td>

                                 <table id="tblProItems"   width="940" border="0" align="center" cellpadding="0" cellspacing="0" >

                                        <tr class="promo-row1">
                                            <td class="tbl04-promo-00-outside">&nbsp;</td>
                                            <td class="tbl04-promo-01-outside"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/></td>
                                            <td class="tbl04-promo-02-outside"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td>
                                            <td class="tbl04-promo-03-outside"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  
                                            <td class="tbl04-promo-04-outside"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td>
                                        </tr>

                                   </table>
                            </td>
                        </tr>
                        </tr>
                        <tr style="display:none;">
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td>

                                <input type="button" value="ADD NEW ITEM" id="addnewProItem" class="add-btn" name="addnewProItem" />

                            </td>
                        </tr>
                        <tr>
                            <td height="20px" style="display:none;">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="20px" style="display:none;">&nbsp;</td>
            </tr>
            <tr>
                <td class="white-bg-style">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td class="line-totals-01">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="height-10px-style">&nbsp;</td>
                        </tr>

                        <tr>
                            <td>

                <table width="940" border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="tbl-totals-01">TOTAL EX GST</td>
                      <td class="tbl-totals-02"><div id="ProfitExGST"></div></td><!--Profit Total-->
                      <td class="tbl-totals-03"><div id="subtotal">$00.00</div></td><!--COG Total-->
                      </tr>
                    <tr>
                      <td class="tbl-totals-04">GST</td>
                      <td class="tbl-totals-05"><div id="profitGST"></div></td>
                      <td class="tbl-totals-06"><div id="gst">$00.00</div></td>
                      </tr>
                    <tr hidden="true">
                      <td width="760" bgcolor="#CCCCCC">Delivery Total</td>
                      <td bgcolor="#CCCCCC">&nbsp;</td>
                      <td bgcolor="#CCCCCC"><div id="deltotal">$00.00</div></td>
                      </tr>
                    <tr>
                      <td class="auto-style6">TOTAL INC GST</td>
                      <td class="auto-style7"><div id="profitFullTotal"></div></td><!--Profit Sub Total-->
                      <td class="auto-style8"><div id="fulltotal">$00.00</div></td><!--COG sub Total-->
                      </tr>
                  
                  </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="height-10px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="line-totals-02">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr style="display:none;">
                <td class="height-10px-style">

                    &nbsp;</td>
            </tr>

            <tr>
                <td><table class="width-940-style" border="0" align="center" cellpadding="0" cellspacing="0">
                          
                  <tr id="TR_singlecommission" runat="server">
              <td width="748" class="comm-01-style"><span id="CD-accountownername"></span>COMMISION &nbsp; DEDUCTION</td>
              <td class="comm-02-style"><div id="totalprofit">$00.00</div></td>
              </tr>
                <tr id="TR_SplitCommission" runat="server">
              <td width="748" class="comm-01-style"><span id="CD-salespersonname"></span>COMMISION &nbsp; DEDUCTION</td>
              <td class="comm-02-style"><div id="totalprofitsalesperson">$00.00</div></td>
              </tr>

                </table></td>
            </tr>
            <tr>
                <td height="20px">&nbsp;</td>
            </tr>
            <tr>
                <td class="white-bg-style">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td class="auto-style10">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style11">NOTES<br /><br />

                                <div id="CN_CreditNotes" runat="server"></div>
                            </td>
                        </tr>

                           <tr>
                            <td class="auto-style11" >

                                <div id="orderNumberDiv" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style10">&nbsp;</td>
                        </tr>

                        <tr>
                            <td class="auto-style12">
                                <div id="ordernotesdiv" runat="server"></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="width-940-style">&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td class="auto-style11">Supplier Notes</td>
                        </tr>
                        <tr>
                            <td class="auto-style10">&nbsp;</td>
                        </tr>
                        <tr style="display:none;">
                            <td class="auto-style12"><div id="suppliernotesdiv" runat="server"></div></td>
                        </tr>
                        <tr>
                            <td class="width-940-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="width-940-style">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="display:none;" height="20px">&nbsp;</td>
            </tr>
            <tr>
                <td style="display:none;" class="align_right">
                    <asp:Button ID="btnOrderSubmit" runat="server" Text="SUBMIT ORDER"  OnClick="btnOrderSubmit_Click"  class="submit-btn" ClientIDMode="Static"/>            
                    <asp:Button ID="btnInvoiceApprove" runat="server" Text="APPROVE ORDER" OnClick="btnInvoiceApprove_Click"  class="submit-btn" Visible="false"/>
                    <input type="button"  id="btnPrint" name="btnPrint" value="PRINT"  class="submit-btn" />
                    <input type="button" id="btnCancelInvoice" name="btnCancelInvoice" value="CANCEL" class="submit-btn" runat="server" />
                    <asp:Button ID="btnAddCreditNote" runat="server" class="add-credit-note-btn" OnClick="btnAddCreditNote_Click" Text=" ADD CREDIT NOTE " Visible="false" />

                </td>
            </tr>

        </table>

    <table id="Table1" width="940" border="0" align="center" cellpadding="0" cellspacing="0"   runat="server" >
        

       
            <tr class="item-supprow">
              <td>
                   <table width="940" border="0" align="center" cellpadding="0" cellspacing="0">

                  </table>
              </td>
            </tr>

            <tr hidden="hidden">
                    <td height="20px">
                        <input type="text" id="testid" runat="server" /> 
                        <input type="text" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" />
                        <input type="text" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" />
                        <input type="text"  id="hdnProCost" name="hdnProCost"  runat="server"/>
                        <input type="text" id="hdnProfit" name="hdnProfit"  runat="server"/>
                        <input type="text" id="hdnContactID" name="hdnContactID" runat="server" />
                        <input type="text" id="hdnCompanyID" name="hdnCompanyID" runat="server" />
                        <input type="text" runat="server" name="OrderItems" id="OrderItems" />
                        <input type="text" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" />
                        <input type="text" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" />
                        <input type="text" runat="server" name="hdnTotal" id="hdnTotal" />
                        <input type="text" runat="server" name="hdnSubTotal" id="hdnSubTotal" />
                        <input type="text" runat="server" name="ProfitTotal" id="ProfitTotal" />
                        <input type="text" runat="server" name="hdnEditOrderItems" id="hdnEditOrderItems" />
                        <input type="text" runat="server" name="hdnEditOrder" id="hdnEditOrder" />
                        <input type="text" runat="server" name="hdnSupplierDelCostItems"    id="hdnSupplierDelCostItems" />
                        <input type="text"  runat="server" name="hdnProItems"  id="hdnProItems" />
                        <input type="text" runat="server" name="CusDelCostItems"  id="CusDelCostItems" />
                        <input type="text" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" />
                        <input type="text" runat="server" name="EditProItems" id="EditProItems" />
                        <input type="text" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" />
                        <input type="text"  runat="server"  name="hdnSupplierNotes" id="hdnSupplierNotes" />
                        <input type="text"  runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" />
                        <input type="text" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" />
                        <input type="text" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" />
                        <input type="text" runat="server" name="hdnAllSuppliers" id="hdnAllSuppliers" />
                        <input type="text" name="hdnAccountOwner" id="hdnAccountOwner"  runat="server" /> 
                        <input type="text" name="navigateURL" id="navigateURL" />
                        <input type="text" name="printscreenURL" id="printscreenURL" />
                        <input type="text" name="hdnORDERID" id="hdnORDERID" runat="server" />
                        <input type="text" name="hdnCommision" id="hdnCommision" runat="server" />
                        <input type="text" name="hdnSTATUS" id="hdnSTATUS"  runat="server" />
                        <input type="text" name="NOTES" id="NOTES"  runat="server"/>
                        <input type="text" name="TempID" id="TempID" runat="server" />
                        CommishSplit
                    <input type="text" name="hdnCommishSplit" id="hdnCommishSplit" runat="server" />
                    AccountOwnerID
                    <input type="text" name="ACCOUNT_OWNER_ID" id="ACCOUNT_OWNER_ID" runat="server" />
                    AccountOwner
                    <input type="text" name="ACCOUNT_OWNER" id="ACCOUNT_OWNER" runat="server" />
                    SalespersonID
                    <input type="text" name="SALESPERSON_ID" id="SALESPERSON_ID" runat="server" />
                    Salesperson
                    <input type="text" name="SALESPERSON_TXT" id="SALESPERSON_TXT" runat="server" />
                    AccountOwnerCommission
                    <input type="text" name="ACCOUNT_OWNER_COMMISH" id="ACCOUNT_OWNER_COMMISH" runat="server" />
                    SalespersonCommission
                    <input type="text" name="SALESPERSON_COMMISH" id="SALESPERSON_COMMISH" runat="server" />
                    SplitVolumeAmount
                    <input type="text" name="VOLUME_SPLIT_AMOUNT" id="VOLUME_SPLIT_AMOUNT" runat="server" />
                        CREDIT_NOTE_ID
                        <input type="text" name="hdnCreditNoteID"  id="hdnCreditNoteID" runat="server" />

                    </td>
   
            </tr>

          </table>


          <div  id="Dialog-Submit-Confirmation" title="SubmitConfirmation" style="display:none;">

              <table>
                <tr>
                     <td>
                            <span id="SubmitMessage" ></span>
                     </td>
                </tr>

                 <tr>
               
                     <td><input type="button" id="btnReturnDashBoard" value="RETURN TO DASHBOARD" /></td>
                     <td><input type="button" id="btnPrintOrder" name="btnPrintOrder"  value="PRINT"  /></td>

                 </tr>
             
              </table>

          </div> 

     

          <div id="Dialog-DeleteConfirmation" style="display:none;">

              <table>

                  <tr>
                      <td>
                           <span id="cancelMessage" ><b>YOU ARE ABOUT TO CANCEL THIS INVOICE. DO YOU WISH TO CONTINUE?</b></span>
                      </td> 
                  </tr>

                  <tr>
                      <td><input type="button" id="btnYES" name="btnYES" value="YES" /></td>
                      <td><input type="button" id="btnNO" name="btnNO" value="NO" /></td>

                  </tr>

              </table>
          </div>

        <div id="Dialog-SuppNotes" style="display:none;">
            <table>
                <tr>
                        <td><input type="text"   id="spSupplierTitle"  name="spSupplierTitle" disabled="disabled" /></td>
                </tr>
                <tr>
                        <td></td>
                </tr>
                <tr>
                    <td><textarea id="taSuppNote" name="taSuppNote"></textarea></td>
                </tr>

                <tr>
                    <td><input type="button" id="submitNote" name="submitNote" value="SUBMIT" /></td>
                    <td><input type="button" id="cancelNote" name="cancelNote" value="CANCEL" /></td>
                </tr>
            </table>
        </div>
            </form>
       </body>
</html>
