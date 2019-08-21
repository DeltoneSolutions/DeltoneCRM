<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCreditNote.aspx.cs" Inherits="DeltoneCRM.CreditNotes.AddCreditNote" MasterPageFile="~/NoNav.Master" %>
<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">

            .promo-row1 {
                background:#cccccc;
            }

           .width-980-style {
            width: 980px;
        }
        .width-940-style {
            width: 940px;
        }
        .width-770-style {
            width: 770px;
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
        .align-vertical {
        vertical-align: top;    
        }
        .white-box-outline-top {
             width: 810px;
             border: 1px solid #cccccc;
             background-color: #ffffff
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
            font-size: 2.1em;
            color: #6b6b6b;
            font-weight: 400;
            letter-spacing: -1px;
        }
        .all-subheadings-style {
            font-family: 'Droid Sans', sans-serif;
            font-size: 1.8em;
            color: #494949;
            font-weight: 400;
            letter-spacing: -1px;
        }
        .company-name-style {
            font-family: 'Droid Sans', sans-serif;
            font-size: 2.5em;
            color: #494949;
            font-weight: 400;
            letter-spacing: -1px;
            vertical-align: top;
        }
        .company-details-style {
            font-family: 'Droid Sans', sans-serif;
            font-size: 16px;
            color: #494949;
            font-weight: 400;
            letter-spacing: -1px;
            height: 25px;
        }
        .company-details-delivery-style {
            font-family: 'Droid Sans', sans-serif;
            font-size: 14px;
            color: #494949;
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
            background-color: #ffffff;
            border: 1px solid #CCCCCC;
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
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #494949;
            font-weight:400;
            letter-spacing: -0.05em;
            background-color: #cccccc;
            height: 30px;
            width: 290px;
            padding-left: 20px;
            border-bottom-color: #ffffff;
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
        .tbl01-clm01-style {
            width: 23px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:center;
            font-weight:400;
            letter-spacing: -0.05em;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
            border-left-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-left-color: #999999;
            border-bottom-color: #999999;
            background:#CCCCCC;
        }
        .tbl01-clm02-style {
            width: 399px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-left: 5px;
        }
        .tbl01-clm03-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-left: 5px;
        }
        .tbl01-clm04-style {
            width: 94px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-left: 5px;
        }
        .tbl01-clm05-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl01-clm06-style {
            width: 29px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl01-clm07-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }

        .tbl01-clm08-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl01-clm09-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
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
	        border-bottom-color: #999999;
            border-left-color: #999999;
            border-right-color: #999999;
            text-align:center;
        }
        .tbl-auto-row-02 {
	        height: 25px;
            width: 404px;
	        border-top-width: 1px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-top-style: solid;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-top-color: #999999;
	        border-bottom-color: #999999;
            border-right-color: #999999;
        }
        .tbl-auto-row-02-btm {
	        height: 25px;
            width: 404px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-bottom-color: #999999;
            border-right-color: #999999;
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
	        border-top-width: 1px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-top-style: solid;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-top-color: #999999;
	        border-bottom-color: #999999;
            border-right-color: #999999;
        }
        .tbl-auto-row-03-btm {
	        height: 25px;
            width: 74px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-bottom-color: #999999;
            border-right-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-04-btm {
	        height: 25px;
            width: 99px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-05-btm {
	        height: 25px;
            width: 74px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-05-inside {
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
        .tbl-auto-row-06 {
	        height: 25px;
            width: 34px;
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-06-btm {
	        height: 25px;
            width: 34px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-07-btm {
	        height: 25px;
            width: 74px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
        }
        .tbl-auto-row-07-inside {
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
        .tbl-auto-row-08 {
	        height: 25px;
            width: 69px;
            font-family: 'Droid Sans', sans-serif !important;
            font-size: 12px;
            color: #494949;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            padding-right: 5px;
            background-color: #eeeeee;
        }

        .tbl02-clm01-style {
            width: 783px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
            border-left-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-left-color: #999999;
            border-bottom-color: #999999;
            background:#CCCCCC;
            padding-left: 5px;
        }
        .tbl02-clm01-style-supp {
            width: 783px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 3px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
            border-left-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-left-color: #999999;
            border-bottom-color: #999999;
            background:#CCCCCC;
            padding-left: 5px;
        }
        .tbl02-clm02-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl02-clm02-style-supp {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 3px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }

        .tbl02-clm03-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl02-clm03-style-supp {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 3px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }

        .comm-01-style {
            font-family: 'Droid Sans', sans-serif !important;
            font-size: 12px;
            color: #fc5c5c;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            width: 800px;
            height: 50px;
            background-color: #eeeeee;
            padding-right: 10px;
            border-top-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
            border-left-color: #fca1a1;
            border-left-style: solid;
	        border-top-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #fca1a1;
            border-bottom-color: #fca1a1;

        }
        .comm-02-style {
            font-family: 'Droid Sans', sans-serif !important;
            font-size: 16px;
            color: #666666;
            text-align:center;
            font-weight:400;
            letter-spacing: -0.05em;
            width: 170px;
            background-color: #fcdddd;
            border-top-style: solid;
            border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #fca1a1;
            border-right-color: #fca1a1;
            border-bottom-color: #fca1a1;
            border-top-width: 1px;
            border-bottom-width: 1px;
            border-right-width: 1px;
        }

            .auto-style1 {
                height: 23px;
            }

/* Delivery Section */

        .tbl-delivery-01-outside {
	        height: 25px;
            width: 788px;
	        border: 1px solid #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            border-left-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            border-left-color: #999999;
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
	        border-top-color: #eeeeee;
	        border-right-color: #eeeeee;
	        border-bottom-color: #eeeeee;
            border-left-color: #eeeeee;
            padding-left: 5px;
            background-color: #eeeeee;
        }
        .tbl-delivery-05-outside {
	        height: 25px;
            width: 69px;
            color: #cccccc;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl-delivery-06-outside-2nd {
	        height: 25px;
            width: 74px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
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
            font-size: 12px;
            color: #666666;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            padding-right: 10px;
            background-color: #e5e4e4;
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
            font-size: 12px;
            color: #666666;
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
            font-size: 12px;
            color: #666666;
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
        .tbl-totals-05 {
	        height: 25px;
            width: 69px;
            font-family: 'Droid Sans', sans-serif !important;
            font-size: 12px;
            color: #666666;
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
            font-size: 12px;
            color: #666666;
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
            font-size: 12px;
            color: #666666;
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
            width: 23px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:center;
            font-weight:400;
            letter-spacing: -0.05em;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
            border-left-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-left-color: #999999;
            border-bottom-color: #999999;
            background:#CCCCCC;
        }
        .tbl04-promo01-style {
            width: 725px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:left;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-left: 5px;
        }
        .tbl04-promo02-style {
            width: 29px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align:right;
            font-weight:400;
            letter-spacing: -0.05em;
            background:#CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl04-promo03-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: right;
            font-weight: 400;
            letter-spacing: -0.05em;
            background: #CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl04-promo04-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: right;
            font-weight: 400;
            letter-spacing: -0.05em;
            background: #CCCCCC;
            border-top-width: 1px;
	        border-right-width: 1px;
            border-bottom-width: 2px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            padding-right: 5px;
        }
        .tbl04-promo-00-outside {
	        height: 25px;
            width: 23px;
	        border: 1px solid #999999;
            text-align: left;
            /*Modifed here Add Background Color*/
             background:#CCCCCC;
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
	        border-left-color: #999999;
	        border-right-color: #999999;
            border-bottom-color: #999999;
            text-align: center;
            cursor: pointer;
            background:#CCCCCC;
        }
        .tbl04-promo-01-outside {
	        height: 25px;
            width: 730px;
	        border-top-width: 1px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-top-style: solid;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-top-color: #999999;
	        border-bottom-color: #999999;
            border-right-color: #999999;
            text-align: left;
        }
        .tbl04-promo-01-outside-2nd {
	        height: 25px;
            width: 730px;
	        border-bottom-width: 1px;
            border-right-width: 1px;
	        border-bottom-style: solid;
            border-right-style: solid;
	        border-bottom-color: #999999;
            border-right-color: #999999;
            text-align: left;
        }
        .tbl04-promo-01-inside {
	        height: 23px;
            width: 723px;
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
            text-align: left;
            padding-left: 5px;
        }
        .tbl04-promo-02-inside {
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
        .tbl04-promo-02-outside {
	        height: 25px;
            width: 34px;
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-02-outside-2nd {
	        height: 25px;
            width: 34px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-03-outside {
	        height: 25px;
            width: 74px;
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-03-outside-2nd {
	        height: 25px;
            width: 74px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-03-inside {
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
        .tbl04-promo-04-outside {
	        height: 25px;
            width: 74px;
	        border-top-width: 1px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-top-style: solid;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-top-color: #999999;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-04-outside-2nd {
	        height: 25px;
            width: 74px;
	        border-right-width: 1px;
	        border-bottom-width: 1px;
	        border-right-style: solid;
	        border-bottom-style: solid;
	        border-right-color: #999999;
	        border-bottom-color: #999999;
            text-align: right;
        }
        .tbl04-promo-04-inside {
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

            .auto-style3 {
                height: 75px;
                width: 520px;
            }

            .auto-style4 {
                width: 290px;
            }

            .auto-style5 {
                width: 150px;
            }

            .auto-style6 {
                border-right: 2px solid #fff;
                height: 29px;
                width: 780px;
                font-family: 'Droid Sans', sans-serif;
                font-size: 12px;
                color: #666666;
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
                font-size: 12px;
                color: #666666;
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
                font-size: 12px;
                color: #666666;
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
            .blackout {
                background-color:#000;
                opacity:.7;
                filter:alpha(opacity=70);
                height:200%;
                width:100%;
                position:fixed;
                top:0;
                left:0;
                z-index:100;
                display:none;
                cursor:pointer;
            }
            .auto-style9 {
                height: 90px;
            }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,500,600,700,800' rel='stylesheet' type='text/css'/>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript">

        var Dialog_Canel; //Cancel Credit Note Dialog Box



        //This function Close the Current Order Window
        function CloseOrderWindow() {
            window.close();
        }

        var Dialog = '';
        var Dialog_Canel = '';
        var Dialog_SuppNote = '';

        var Dialog_OrderClose = ''

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
            $('#Dialog-SuppNotes').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }
        //End Method Load Suppliert Note Window


        //Function CloseNote Dialog
        function CloseNoteDialog() {
            Dialog_SuppNote.dialog('close');
        }
        //End Mehtod CloseNote Dialog


        //CLose the Order Window
        function Close_OrderWindow() {
            Dialog_OrderClose.dialog('close');
        }


        /*CLOSE Order window Initialization Dialog Window*/
        function CLOSE_ORDERDIALOG() {
            Dialog_OrderClose = $('#Dialog_CloseOrderWindow').dialog({
                resizable: false,
                modal: true,
                title: 'CLOSE ORDER CONFIRMATION',
                height: 400,
                width: 710
            });
            $('#Dialog_CloseOrderWindow').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }
        /*CLOSE Order window Initialization Dialog Window*/


        //This Function Initialize the Delete CREDIT NOTE  Dialog Window
        function CancelInvoiceDialog() {
            Dialog_Canel = $('#DialogCancelCreditNote').dialog({
                resizable: false,
                modal: true,
                title: 'CANCEL CREDIT NOTE CONFIRMATION',
                height: 400,
                width: 710
            });

            $('#DialogCancelCreditNote').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }
        //END delete INVOICE Window


        //This Function Close the Delete  CREDIT NOTE Dialog Window
        function CloseCancelInvoiceDialog() {
            Dialog_Canel.dialog('close');
        }
        //END Function Close Delete CREDIT Note  Window

        //This Function Inititalize the Submit DialogWindow
        function SubmitDialog(Message, url, PrintScreenUrl) {
            $('#SubmitMessage').html(Message);
            //Set the Navogation URL
            $('#navigateURL').val(url);
            //Set the Print Screen URL
            //$('#printscreenURL').val(PrintScreenUrl);


            Dialog = $('#Dialog-Submit-Confirmation').dialog({
                resizable: false,
                modal: true,
                title: 'SUBMIT CONFIRMATION',
                height: 400,
                width: 710
            });

            $('#Dialog-Submit-Confirmation').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
                //location.reload();
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
                    PopulateSupplierNotesTable();
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

                $('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

            }
            if (QImagingCount > 0) {
                //Add a Supplier Delivry Row here Q-Imaging 
                var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-04-inside" id="suppdeldet" value="Q-Imaging"/></td><td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td></tr>');
                $('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

            }

        }


        //Modification done here Migrating new changes 16/6/2015 NOT IN USE
        function SuppDelTable_OLD() {

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
                        PopulateSupplierNotesTable();
                    }
                });
            }


            if (QImagingCount == 0) {

                $('#tblSupplierDeliveryCost tr').each(function () {

                    if ($(this).find('input[name="suppdeldet"]').val() == 'Q-Imaging') {
                        $(this).remove();
                        PopulateSupplierNotesTable();
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

            //$('#<%=TempID.ClientID%>').val(Temp_ID);

            var now = new Date();
            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);

            var today = now.getFullYear() + "-" + (month) + "-" + (day);

            $('#<%=datereceived.ClientID%>').val(today);

            /*Close the Current Order Window*/
            $('#btn_CloseWindow_YES').click(function () {
                window.close();
            });

            /*Close the Current Dialog Window*/
            $('#btn_CloseWindow_NO').click(function () {
                Close_OrderWindow();
            });

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
                $('.blackout').css("display", "block");
                CancelInvoiceDialog();
            });


            /*Cancel Order Button Click Event Hanlder*/
            $('#btnCreditYES').click(function () {
                var CreditNoteID = $('#<%=hdnCreditNoteID.ClientID%>').val();
                var ContactID = $('#<%=hdnContactID.ClientID%>').val();
                var CompanyID = $('#<%=hdnCompanyID.ClientID%>').val();
                var PreviousStatus = $('#<%=ORDER_STATUS.ClientID%>').val();


                if (CreditNoteID) {
                    $.ajax({
                        type: "POST",
                        url: "process/ProcessCancelCreditNote.aspx",
                        data: {
                            creditnoteid: CreditNoteID,
                            PreviousStatus: PreviousStatus,
                        },
                        success: function (msg) {
                            if (msg) {

                                if (msg == 'SUCCESS') {
                                    window.location.href = "CreditNotes/AllCreditNotes.aspx";
                                }
                                else {
                                    alert('ERROR  DELETING CREDIT NOTE');
                                    Dialog_Canel.dialog('close');
                                }

                            }

                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                }

            });

            $('#btnCreditNO').click(function () {
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

            /*Function Reposible of repopulation of Supplier Delivery Tables*/
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

                            var clientfunction = "LoadSuppNote('" + arr_Suppliers[k] + "');";
                            var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-01-outside-2nd"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-01-inside" id="suppdeldet" value=' + arr_Suppliers[k] + '></td><td align="right" class="tbl-delivery-05-outside-2nd"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td align="right" class="tbl-delivery-06-outside-2nd"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost_' + k + '" value="' + RowDelCost + '"></td></tr>');
                            $('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                        }

                    }

                }

                //MODIFIED HERE APPLY BIND FUNCTION TO ALL DELIVERY ROWS
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                    var $row_test = $(row);

                    $row_test.find('input[name*="suppdelcost"]').bind('blur', function () {
                        if (isNaN($row_test.find('input[name*="suppdelcost"]').val())) {
                            $row_test.find('input[name*="suppdelcost"]').val("0.00"); //SET TO DEFAULT VALUE
                        }
                        update_price();
                    });

                });
                //END MODIFICATION BIND BLUR FUNCTIONS 


                //Populate the Supplier Notes Table
                PopulateSupplierNotesTable();

            }
            //End Function Repopulation of Supplier Delivery Table

            function Repopulate() {

                Suppliers = $(' #<%=hdnAllSuppliers.ClientID %>').val();
                var arr_Suppliers = Suppliers.split(':');
                $('#tblSupplierDeliveryCost').html('');

                for (k = 0; k < arr_Suppliers.length; k++) {
                    if (arr_Suppliers[k] != '') {

                        if (IsSupplierExsists(arr_Suppliers[k]) > 0) {
                            //Add a Supplier Row here 
                            var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" type="text" disabled="disabled" class="tbx_cust_delivery" id="suppdeldet" value=' + arr_Suppliers[k] + '></td><td align="right" class="tbx_supp_delivery_cost_na"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true">N/A</td><td align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td></tr>');
                            $('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                            $SuppDelCost_Row.find('input[name="suppdelcost"]').blur(function () {


                                if (isNaN($SuppDelCost_Row.find('input[name="suppdelcost"]'))) {
                                    $SuppDelCost_Row.find('input[name="suppdelcost"]').val("0.00");
                                }

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
                            AddProItemFirstrow(arr_ProItem[0], arr_ProItem[1], arr_ProItem[2], arr_ProItem[3], arr_ProItem[4]);
                        }
                        if (i != 0) {
                            //Add it the subsequent rows
                            AddProItemRow(arr_ProItem[0], arr_ProItem[1], arr_ProItem[2], arr_ProItem[3], arr_ProItem[4]);
                        }
                    }
                }

            }

            /* This Function Add promotional First Row */
            function AddProItemFirstrow(proitem, procost, quantity, shippingcost, promocode) {
                $('#promoitem').val(proitem);
                $('#promocost').val(parseFloat(procost).toFixed(2));
                $('#promoqty').val(quantity);
                $('#shippingCost').val(parseFloat(shippingcost).toFixed(2));
                //Modified here To Add promoCode 
                $('#promoCode').val(promocode);
                //End Adding promo code
            }

            /*Bind the Promotional Item values*/
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
                        //Modified here Add promoCode 
                        $Pro_Row.find('#promoCode').val(ui.item.promocode);
                        //End Modification here
                        $Pro_Row.find('#hidden_promo_item_id').val(ui.item.id);
                        $Pro_Row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        event.preventDefault();
                    }
                });


                $Pro_Row.find('#promocost').blur(update_price);
                $Pro_Row.find('#promoqty').blur(update_price);
                //$Pro_Row.find('#promoqty').blur(update_price);
                $Pro_Row.find('#shippingCost').blur(update_price);
            }


            function AddProItemRow(proitem, procost, quantity, shippingcost, promoCode) {

                //Modified here Add promoitemCode <td>
                $row_proItem = $('<tr class="promo-row1"><td class="tbl04-promo-00-outside"><a class="delete_proitem" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl04-promo-01-outside"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/></td><td class="tbl04-promo-03-outside"><input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td><td class="tbl04-promo-02-outside"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td><td class="tbl04-promo-03-outside"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  <td class="tbl04-promo-04-outside"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td></tr>').insertAfter(".promo-row1:last");
                $row_proItem.find('#promoitem').val(proitem);
                $row_proItem.find('#promocost').val(parseFloat(procost).toFixed(2));
                $row_proItem.find('#promoqty').val(quantity);
                $row_proItem.find('#shippingCost').val(parseFloat(shippingcost).toFixed(2));
                $row_proItem.find('#promoCode').val(promoCode);
                //Modified here to add promocode 
                bindPromoItems($row_proItem);

            }
            /*End Promotional Items Section*/


            //Fill the Order Items 
            function fillOrderItems(OrderItems, Order, SuppNotes) {

                //Modification done here Set the OrderDate
                //var today = now.getFullYear() + "-" + (month) + "-" + (day);

                $('#<%=datereceived.ClientID%>').val($('#<%=ORDER_DATE.ClientID%>').val());
                //Modified here Add Order Created Date
                //$('#<%=datecreated.ClientID%>').val($('#<%=ORDER_CREATE_DATE.ClientID%>').val());


                var arr_OrderItems = OrderItems.split("|");
                var arr_Order = Order.split(":");

                //Modification done 1/5/2015  SupplierNotes Population

                var arr_suppnotes = SuppNotes.split("|");
                for (i = 0; i < arr_suppnotes.length; i++) {
                    if (arr_suppnotes[i]) {
                        var note = arr_suppnotes[i].split(':');
                        $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes" style="width:780px; height:100px;"></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');
                        $('#tblSupplierNotes').append($row);
                        $row.find('#suppName').val(note[0]);
                        $row.find('#taSuppNotes').val(note[1]);
                    }

                }

                //End SupplierNotes 

                //For the First Item Fill the Table First Row
                var arr_FirstOrderItem = arr_OrderItems[0].split(",");

                $('#ItemDesc').val(arr_FirstOrderItem[0]);
                $('#suppliercode').val(arr_FirstOrderItem[3]);
                $('#COG').val(parseFloat(arr_FirstOrderItem[2]).toFixed(2));
                $('#qty').val(arr_FirstOrderItem[4]);
                $('#UnitPrice').val(parseFloat(arr_FirstOrderItem[1]).toFixed(2));
                $('#hidden_item_code').val('');
                $('#hidden_Supplier_Name').val(arr_FirstOrderItem[5]);
                //Modified here Add Supplier Name instead of Original/Compatible
                $('#ItemType').val(arr_FirstOrderItem[5]);


                //Update the Price for the First Row
                update_price_firstrow();

                //For the Susquent Elements 
                for (j = 1; j < arr_OrderItems.length; j++) {
                    if (arr_OrderItems[j]) {

                        var arr_OrderItem = arr_OrderItems[j].split(",");
                        var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm" ><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                        $row.find('#ItemDesc').val(arr_OrderItem[0]);
                        $row.find('#suppliercode').val(arr_OrderItem[3]);
                        $row.find('#COG').val(parseFloat(arr_OrderItem[2]).toFixed(2));
                        $row.find('#qty').val(arr_OrderItem[4]);
                        $row.find('#UnitPrice').val(parseFloat(arr_OrderItem[1]).toFixed(2));
                        $row.find('#hidden_item_code').val();
                        $row.find('#hidden_Supplier_Name').val(arr_OrderItem[5]);
                        //Modified here Add Supplier Name instead of Original/Compatible
                        $row.find('#ItemType').val(arr_OrderItem[5]);


                        //Update the Price for that Row 
                        update_price_row($row);
                        //Bind Row Functonalities for Austopopulations ,prince updation ,etc
                        bind($row);
                    }

                }
                //End populating for Susquent Elements

                //EditSuppCostItems,EditProItems,EditCusDelCostItems
                //var SuppDelCostItems = arr_Order[0];
                var SuppDelCostItems = arr_Order[6]; //Supplier  DelItems value
                var ProItems = arr_Order[1];
                //var CusDelCostItems = arr_Order[2];
                var CusDelCostItems = arr_Order[5];//Customer DelCost Item value

                //Set Customer DelCost Items

                //var arr_CusDelCost = arr_Order[2].split("|");
                var arr_CusDelCost = arr_Order[5].split("|");

                //Customer Delivery Cost 
                $('#deldet').val(arr_CusDelCost[0]);
                //Modification done here format delivery cost for 2 decimal places.
                $('#delcost').val(arr_CusDelCost[1]);

                //Set Promotional Cost Items

                var arr_ProitemCost = ProItems.split("|");

                //Populate Promotional Items  
                if ($('#<%=hdnEditproitpems.ClientID %>').val()) {
                    populateProItems($('#<%=hdnEditproitpems.ClientID %>').val());
                }

                //Supplier Delivery Cost Population
                arr_SuppDelCost = SuppDelCostItems.split("|");
                //For the Fist Row
                var arr_SuppDelCost_FirstRow = arr_SuppDelCost[0].split(",");

                $('#suppdeldet').val(arr_SuppDelCost_FirstRow[0]);
                $('#suppdelcost').val(arr_SuppDelCost_FirstRow[1]);

                //For SubSquent SuppDelCost Rows 
                for (k = 1; k < arr_SuppDelCost.length; k++) {
                    if (arr_SuppDelCost[k]) {

                        var arr_SuppDelCost_Row = arr_SuppDelCost[k].split(',');
                        var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" disabled="disabled" type="text" class="tbx_cust_delivery" id="suppdeldet"></td><td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td><td width="76" align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td><td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td><td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td></tr>').insertAfter(".supp-del-row:last");
                        $SuppDelCost_Row.find('#suppdeldet').val(arr_SuppDelCost_Row[1]);
                        $SuppDelCost_Row.find('#suppdelcost').val(arr_SuppDelCost_Row[2]);


                        //Modification done here 
                        $SuppDelCost_Row.find('#suppdelcost').blur(function () {

                            if (isNaN($SuppDelCost_Row.find('#suppdelcost').val())) {
                                $SuppDelCost_Row.find('#suppdelcost').val("0.00");
                            }
                        });

                    }
                }
            }

            //Edit Functionalities
            if ($('#<%=hdnEditOrderItems.ClientID%>').val() != '') {


                //Display the Print Button
                //$('#btnPrint').show();

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
                    //Modified here SupplieName 
                    myData.push($row.find('input[name*="ItemType"]').val());
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

                    SuppDelItems.push($row.find('input[name*="suppdeldet"]').val());
                    SuppDelItems.push($row.find('input[name*="suppdelcost"]').val());
                    SuppDelItems.push("|");
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
                    ProItems.push($row_ProItems.find('#promoCode').val());

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
                    //Modified here Add PromoCode 
                    var $row = $('<tr class="promo-row1"><td class="tbl04-promo-00-outside"><a class="delete_proitem" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl04-promo-01-outside"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/></td><td class="tbl04-promo-03-outside"><input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td><td class="tbl04-promo-02-outside"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td><td class="tbl04-promo-03-outside"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  <td class="tbl04-promo-04-outside"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td></tr>').insertAfter(".promo-row1:last");
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
                    var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
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
                            PopulateSupplierNotesTable();
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
                        $Pro_Row.find('#promocost').val(parseFloat(ui.item.promocost).toFixed(2));
                        $Pro_Row.find('#promoqty').val(ui.item.qty);
                        $Pro_Row.find('#shippingCost').val(parseFloat(ui.item.shippingcost).toFixed(2));
                        //Shipping Cost
                        $Pro_Row.find('#hidden_promo_item_id').val(ui.item.id);
                        $Pro_Row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        $Pro_Row.find('#promoCode').val(ui.item.promocode);
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
                        $row.find('#ItemType').val(ui.item.SupplierName);
                        return false;
                    },
                    select: function (event, ui) {
                        $row.find('#ItemDesc').val(ui.item.Description);
                        $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                        $row.find('#hidden_item_code').val(ui.item.ItemID);
                        //Set the Hidden supplier Name for the Row
                        $('#hdn_Supp_Name').val(ui.item.SupplierName);
                        $row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        $row.find('#ItemType').val(ui.item.SupplierName); //Add Supplier Name

                        if (!(findRowExsists(ui.item.SupplierName))) {
                            SuppDelTable();
                        }

                        $row.find('#COG').val(parseFloat(ui.item.COG).toFixed(2));  //MODIFIED HERE  ROUND
                        $row.find('#UnitPrice').val(parseFloat(ui.item.ManagerUnitPrice).toFixed(2)); //MODIFIED HERE ROUND
                        $row.find('#qty').val(1);
                        return false;
                    }

                });

            }
            //End Function Bind AutoCompletion and Uppate Cells


            //Supplier Delivery Cost Bind function 
            function BindSupplierDelivery($row) {

                $row.find('input[name="suppdelcost"]').blur(function () {
                    (update_price);
                });

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
                    //Modified here Add PromoCode here and Shipping Cost
                    $('#promoCode').val(ui.item.promocode);
                    $('#shippingCost').val(parseFloat(ui.item.shippingcost).toFixed(2));
                    //parseFloat(price).toFixed(2)
                    //End Modification
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
                    //$('#delcost').val(ui.item.deliverycost);
                    $('#delcost').val(parseFloat(ui.item.deliverycost).toFixed(2));
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
                    $('#ItemType').val(ui.item.SupplierName);
                    return false;
                },
                select: function (event, ui) {
                    $('#ItemDesc').val(ui.item.Description);
                    $('#suppliercode').val(ui.item.SupplierItemCode);
                    $('#hidden_item_code').val(ui.item.ItemID);
                    $('#hdn_Supp_Name').val(ui.item.SupplierName); //Test
                    $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                    $('#COG').val(parseFloat(ui.item.COG).toFixed(2));   //MODIFIED HERE ROUND 
                    $('#UnitPrice').val(parseFloat(ui.item.ManagerUnitPrice).toFixed(2)); // MODIFIED HERE ROUND
                    $('#qty').val(1);
                    $('#ItemType').val(ui.item.SupplierName);
                    //Modified Here Add Supplier Name instead of Item Type
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
            $('#suppdelcost').blur(function () {

                //alert('Triggered');

                if (isNaN($('#suppdelcost').val())) {
                    $('#suppdelcost').val("0.00");
                }

                update_price();
            });

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

            ///Calculate the Order Total 

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

                    if (!isNaN(SupplierDeliverCost)) SupplierDeliverCost += Number($(this).find('input[name="suppdelcost"]').val());
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
                Commission_Deduction = ((parseFloat(TotalProfitINCGST).toFixed(2) - parseFloat(TotalCOGINCGST).toFixed(2)) * 0.4).toFixed(2);



                thedeltotal = $('#deltotal').html().replace("$", "");

                ordertotal = parseFloat(thedeltotal) + fulltotal;//Oreder COG Total
                $('#ordertotal').html("$" + parseFloat(ordertotal).toFixed(2));//Ordet COG Total


                //Modification donen here Calculate the Commision based on Excluding GST
                //var Commission = (parseFloat(exgsttotal) * 0.4).toFixed(2);

                //var CommisonDeduction=-(parseFloat(Commission).toFixed(2));

                var CommisonDeduction = (Commission_Deduction);
                CommisonDeduction = (CommisonDeduction / 1.1);

                $('#totalprofit').html("$-" + (parseFloat(CommisonDeduction).toFixed(2)));
                $('#<%=hdnCommision.ClientID%>').val(parseFloat(CommisonDeduction).toFixed(2));

            };
        });
        //End Calculating Order Total

    </script>

</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="blackout">
</div>
   <table align="center" cellpadding="0" cellspacing="0" class="btm_01">
        <tr>
            <td>

                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td id="OrderTitle" runat="server" class="all-headings-style"  style="display:none;">NEW CREDIT NOTE</td>
            <td id="QorN"  runat="server">&nbsp;</td>   
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td class="white-box-outline-top">
                            <table align="center" cellpadding="0" cellspacing="0" class="width-770-style">
                                <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td class="auto-style3">
                                        <table align="left" cellpadding="0" cellspacing="0" class="width-470-style">
                                            <tr>
                                                <td class="company-name-style" height="30px"><div id="CompanyNameDIV" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="ContactInfo" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="StreetAddressLine1" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="StreetAddressLine2" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-tel-style"><div id="ContactandEmail" runat="server"></div></td>
                                            </tr>

                                        </table>
                                    </td>
                                    <td class="">
                                        <table align="center" cellpadding="0" cellspacing="0" class="auto-style4"  style="display:none;">
                                            <tr>
                                                <td class="delivery-add-heading-style">DELIVERY ADDRESS</td>
                                            </tr>
                                            <tr>
                                                <td>
                                        <table align="center" cellpadding="0" cellspacing="0" class="width-250-style">
                                            <tr>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryContact" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryCompany" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryAddressLine1" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryAddressLine2" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;

                                                    
                                                </td>
                                             
                                            </tr>
                                        </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                                               <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td width="20px">&nbsp;</td>
                        <td width="150px" class="align-vertical">
                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style5">
                                <tr>
                                    <td class="top-payment-terms-heading" style="display:none;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="display:none;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px"><b>TYPE OF CREDIT</b></td>
                                </tr>
                                <tr>
                                    <td height="16px">
                                        <input type="text" id="RefID" name="RefID" value="" runat="server"   disabled="disabled" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px" style="display:none;">TYPE OF CALL</td>
                                </tr>
                                <tr>
                                    <td height="16px" style="display:none;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px"  style="display:none;">INVOICE DATE</td>
                                </tr>
                                <tr>
                                    <td height="16px" style="display:none;">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;<b>CREATED BY</b></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlUsers" runat="server" CssClass="account-owner-drop">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td  id="tdOrderCreateDate" class="top-payment-terms-heading" runat="server" style="display:none;">&nbsp;&nbsp;&nbsp;&nbsp;<b>CREATED DATE</b></td>
                                </tr>
                                
                                <tr>
                                    <td height="16px">
                                        <input name="datecreated" type="date" class="FormDateValues" id="datecreated"   disabled="disabled" value="" runat="server"  style="display:none;"/>
                                    </td>
                                </tr>

                               
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td> 
                                        <br />
                                        
                                       <asp:Button ID="btnInvoiceApprove" runat="server" Text="APPROVE CREDIT NOTE" OnClick="btnInvoiceApprove_Click"  class="submit-btn" Style="display:none;"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>



        <tr>
            <td height="20px">&nbsp;</td>
        </tr>

    </table>



            </td>
        </tr>
    </table>
   
    
    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">

        <tr>
            <td class="white-bg-style" height="50px">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="all-subheadings-style">Products</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl01-clm01-style">X<br />
&nbsp;</td>
                                    <td class="tbl01-clm02-style">ITEM DESCRIPTION<br />
&nbsp;</td>
                                    <td class="tbl01-clm03-style" >SUPPLIER NAME<br />
&nbsp;</td>
                                    <td class="tbl01-clm04-style">SUPPLIER<br />
                                        CODE</td>
                                    <td class="tbl01-clm05-style">COG
                                        <br />
                                        EX GST</td>
                                    <td class="tbl01-clm06-style">QTY<br />
&nbsp;</td>
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
              <td class="tbl-auto-row-03"><label for="suppliercode" >
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
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <input name="addnewitem" type="button" class="add-btn" id="addnewitem" value="ADD NEW ITEM"/>

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
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
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style" style="display:none;">Delivery</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>



                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style"  >
                                <tr>
                                    <td class="tbl02-clm01-style">CUSTOMER DELIVERY TYPE<br />
&nbsp;</td>
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
                    N/A<input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden"/>
                </td>
            </tr>
        </table>

                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                               <table align="center" cellpadding="0" cellspacing="0" class="width-940-style"  >
                                <tr>
                                    <td class="tbl02-clm01-style-supp">DELIVERY FROM SUPPLIER<br />
&nbsp;</td>
                                    <td class="tbl02-clm02-style-supp">&nbsp;</td>
                                    <td class="tbl02-clm03-style-supp">COST<br />
                                        EX GST</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblSupplierDeliveryCost" width="940" border="0" align="center" cellpadding="0" cellspacing="0" >
                              <tr class="supp-del-row">
                                <td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="suppdeldet"  disabled="disabled"></td>
                                <td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden"/><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" size="1" hidden="hidden" />N/A</td>
                                <td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"/></td>
                                </tr>

                          </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <tr  class="item-supprow">
                <td>
                    <table id="tblSupplierNotes" >

                    </table>
                </td>
             </tr>

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
        <tr>
            <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style" style="display:none;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Promotional</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td>

                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl04-promo00-style">X<br />
&nbsp;</td>
                                    <td class="tbl04-promo01-style">ITEM DESCRIPTION<br />
&nbsp;</td>

                                    <td class="tbl04-promo03-style">ITEM CODE<br />&nbsp;</td>

                                    <td class="tbl04-promo02-style">QTY<br />
&nbsp;</td>
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
                                        <td class="tbl04-promo-01-outside"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/>
                                            <input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/>
                                        </td>
                                        <td class="tbl04-promo-03-outside"><input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td>
                                        <td class="tbl04-promo-02-outside"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td>
                                        <td class="tbl04-promo-03-outside"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  
                                        <td class="tbl04-promo-04-outside"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td>
                                    </tr>

                               </table>
                        </td>
                    </tr>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <input type="button" value="ADD NEW ITEM" id="addnewProItem" class="add-btn" name="addnewProItem" />

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
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
                    <tr>
                        <td height="20px">&nbsp;</td>
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
                        <td height="20px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td height="20px">

                &nbsp;</td>
        </tr>

        <tr>
            <td><table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
                          
                <tr>
              <td width="748" class="comm-01-style">COMMISION &nbsp; DEDUCTION</td>
              <td class="comm-02-style"><div id="totalprofit">$00.00</div></td>
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
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Notes</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                                     <asp:TextBox ID="OrderNotes" TextMode="multiline" class="notes-textbox-style" runat="server"></asp:TextBox>

                                 </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style9"></td>
        </tr>
        <tr>
            <td class="align_right">
                <asp:Button ID="btnOrderSubmit" runat="server" Text="SAVE"  OnClick="btnOrderSubmit_Click"  class="submit-btn" ClientIDMode="Static"  /> 
                <asp:Button  ID="btnGeneratePDF" runat="server" Text="PDF" OnClick="btnGeneratePDF_Click" class="submit-btn" />
                <asp:Button ID="btnCloseOrderWindow"   runat="server" Text="CLOSE" CssClass="submit-btn"    ClientIDMode="Static"  OnClick="btnCloseOrderWindow_Click" />  
                <input type="button"  id="btnPrint" name="btnPrint" value="PRINT"  class="submit-btn"  />
                <input type="button" id="btnCancelInvoice" name="btnCancelInvoice"  value="CANCEL CREDIT NOTE" class="submit-btn"  runat="server" />
            </td>
        </tr>

    </table>

<table id="Table1" width="980" border="0" align="center" cellpadding="0" cellspacing="0"   runat="server" >
        

       
        <tr class="item-supprow">
          <td>
               <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">

              </table>
          </td>
        </tr>

        <tr>
                <td height="20px">
                    <input type="hidden" id="testid" runat="server" /> 
                    <input type="hidden" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" />
                    <input type="hidden" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" />
                    <input type="hidden"  id="hdnProCost" name="hdnProCost"  runat="server"/>
                    <input type="hidden" id="hdnProfit" name="hdnProfit"  runat="server"/>
                    <input type="hidden" id="hdnContactID" name="hdnContactID" runat="server" />
                    <input type="hidden" id="hdnCompanyID" name="hdnCompanyID" runat="server" />
                    <input type="hidden" runat="server" name="OrderItems" id="OrderItems" />
                    <input type="hidden" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" />
                    <input type="hidden" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" />
                    <input type="hidden" runat="server" name="hdnTotal" id="hdnTotal" />
                    <input type="hidden" runat="server" name="hdnSubTotal" id="hdnSubTotal" />
                    <input type="hidden" runat="server" name="ProfitTotal" id="ProfitTotal" />
                    <input type="hidden" runat="server" name="hdnEditOrderItems" id="hdnEditOrderItems" />
                    <input type="hidden" runat="server" name="hdnEditOrder" id="hdnEditOrder" />
                    <input type="hidden" runat="server" name="hdnSupplierDelCostItems"    id="hdnSupplierDelCostItems" />
                    <input type="hidden"  runat="server" name="hdnProItems"  id="hdnProItems" />
                    <input type="hidden" runat="server" name="CusDelCostItems"  id="CusDelCostItems" />
                    <input type="hidden" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" />
                    <input type="hidden" runat="server" name="EditProItems" id="EditProItems" />
                    <input type="hidden" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" />
                    <input type="hidden"  runat="server"  name="hdnSupplierNotes" id="hdnSupplierNotes" />
                    <input type="hidden"  runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" />
                    <input type="hidden" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" />
                    <input type="hidden" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" />
                    <input type="hidden" runat="server" name="hdnAllSuppliers" id="hdnAllSuppliers" />
                    <input type="hidden" name="hdnAccountOwner" id="hdnAccountOwner"  runat="server" /> 
                    <input type="hidden" name="navigateURL" id="navigateURL" />
                    <input type="hidden" name="printscreenURL" id="printscreenURL" />
                    <input type="hidden" name="hdnORDERID" id="hdnORDERID" runat="server" />
                    <input type="hidden" name="hdnCommision" id="hdnCommision" runat="server" />
                    <input type="hidden" name="hdnSTATUS" id="hdnSTATUS"  runat="server" />
                    <input type="hidden" name="NOTES" id="NOTES"  runat="server"/>
                    <input type="hidden" name="TempID" id="TempID" runat="server" />
                    <input type="hidden" name="ORDER_STATUS" id="ORDER_STATUS" runat="server"/>
                    <input type="hidden" name="ORDER_DATE"  id="ORDER_DATE"  runat="server" />
                    <input type="hidden" name="ORDER_CREATE_DATE" id="ORDER_CREATE_DATE" runat="server" />

                    <input type="hidden" name="hdnCreditNoteID" id="hdnCreditNoteID" runat="server" />

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
               
                 <td><input type="button" id="btnReturnDashBoard" value="RETURN TO CREDIT NOTES" /></td>
                 <td><input type="button" id="btnPrintOrder" name="btnPrintOrder"  value="PRINT"  style="display:none;" /></td>

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

    <div id="Dialog_CloseOrderWindow" style="display:none;">

        <table>
            <tr>
                <td>ARE YOU SURE YOU WANT TO CLOSE THIS ORDER?</td>
            </tr>
            <tr>
              <td><input type="button" id="btn_CloseWindow_YES"  name="btn_CloseWindow_YES"  value="YES"/></td>
              <td><input type="button" id="btn_CloseWindow_NO" name="btn_CloseWindow_NO" value="NO" /></td>

             

            </tr>

        </table>
    </div>


    <div id="DialogCreditType" style="display:none;">

        <table>
            <tr>

                <td>TYPE OF CREDIT</td>
            </tr>

            <tr>
                <td>


                </td>
            </tr>


        </table>

    </div>

    <div id="DialogCancelCreditNote" style="display:none;">
         <table>
            <tr>

                <td>ARE YOU SURE YOU WANT TO CANCEL THE CREDIT NOTE?</td>
            </tr>

            <tr>
                <td>
                     <input type="button"   id="btnCreditYES"   value="YES" />
                     <input type="button"   id="btnCreditNO"   value="NO" />
                </td>
            </tr>
           
        </table>

     </div>





</asp:Content>
