<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="UpdateCredit.aspx.cs" Inherits="DeltoneCRM.WebForm1" %>

<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        body {
            background-color: #FFFFCC !important;
        }

        .promo-row1 {
            background: #cccccc;
        }

        .width-980-style {
            width: 1200px;
        }

        .width-940-style {
            width: 1100px;
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
            background-color: #ffffff;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
            letter-spacing: -0.05em;
            border: 1px solid #eb473d;
            background-color: #ffffff;
        }

            .submit-btn:hover {
                color: #ffffff;
                font-weight: 400;
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
            font-weight: 400;
            letter-spacing: -0.05em;
            border: 1px solid #eb473d;
            background-color: #ffffff;
        }

            .add-btn:hover {
                color: #ffffff;
                background-color: #eb473d;
                cursor: pointer;
            }

        .payment-terms-drop {
            width: 150px;
            height: 30px;
            margin-bottom: -1px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #494949;
            font-weight: 400;
            letter-spacing: -0.05em;
            border: 2px solid #cde9fe;
            -webkit-appearance: none;
            text-align: center;
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
            font-weight: 400;
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
            text-align: center;
            font-weight: 400;
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
            background: #CCCCCC;
        }

        .tbl01-clm02-style {
            width: 399px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: left;
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
            padding-left: 5px;
        }

        .tbl01-clm03-style {
            width: 69px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: left;
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
            padding-left: 5px;
        }

        .tbl01-clm04-style {
            width: 94px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: left;
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
            padding-left: 5px;
        }

        .tbl01-clm05-style {
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

        .tbl01-clm06-style {
            width: 29px;
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

        .tbl01-clm07-style {
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

        .tbl01-clm08-style {
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

        .tbl01-clm09-style {
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
            text-align: center;
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
            width: 87%;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #494949;
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: left;
            font-weight: 400;
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
            background: #CCCCCC;
            padding-left: 5px;
        }

        .tbl02-clm01-style-supp {
            width: 783px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: left;
            font-weight: 400;
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
            background: #CCCCCC;
            padding-left: 5px;
        }

        .tbl02-clm02-style {
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

        .tbl02-clm02-style-supp {
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

        .tbl02-clm03-style-supp {
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
            text-align: right;
            font-weight: 400;
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
            text-align: center;
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: center;
            font-weight: 400;
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
            background: #CCCCCC;
        }

        .tbl04-promo01-style {
            width: 725px;
            height: 35px;
            font-family: 'Droid Sans', sans-serif;
            font-size: 11px;
            color: #000000;
            text-align: left;
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
            padding-left: 5px;
        }

        .tbl04-promo02-style {
            width: 29px;
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
            background: #CCCCCC;
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
            background: #CCCCCC;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            text-align: right;
            font-weight: 400;
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
            background-color: #000;
            opacity: .7;
            filter: alpha(opacity=70);
            height: 200%;
            width: 100%;
            position: fixed;
            top: 0;
            left: 0;
            z-index: 100;
            display: none;
            cursor: pointer;
        }

        .auto-style9 {
            height: 90px;
        }

        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .moveRight {
            float: right;
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 200px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css' />
    <link href='http://fonts.googleapis.com/css?family=Raleway:400,300,500,600,700,800' rel='stylesheet' type='text/css' />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript">

        var Dialog_Canel; //Cancel Credit Note Dialog Box
        var canCall = '<%= userlevel %>';  

        $(document).ready(function () {

            if (canCall)
                $(".addproduct").show();
            else
                $(".addproduct").hide();

        });
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

        //function getParameterByName(name) {
        //    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        //    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        //        results = regex.exec(location.search);
        //    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        //}

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
            $('#TR_SalespersonCommission').hide();


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


            ///FILL CREDIT NOTE ITEMS
            function fillOrderItems(OrderItems, Order, SuppNotes) {


                $('#<%=datereceived.ClientID%>').val($('#<%=ORDER_DATE.ClientID%>').val());
                //Modified here Add Order Created Date
                //$('#<%=datecreated.ClientID%>').val($('#<%=ORDER_CREATE_DATE.ClientID%>').val());


                var arr_OrderItems = OrderItems.split("|");
                var arr_Order = Order.split(":");


                var arr_suppnotes = SuppNotes.split("|");
                for (i = 0; i < arr_suppnotes.length; i++) {
                    if (arr_suppnotes[i]) {
                        var note = arr_suppnotes[i].split(':');
                        $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes" style="width:780px; height:100px;"></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');
                        
                            //Modified NOT IN USE

                        //$('#tblSupplierNotes').append($row);
                        //$row.find('#suppName').val(note[0]);
                        //$row.find('#taSuppNotes').val(note[1]);
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
                $('#ItemType').val(arr_FirstOrderItem[5]);
                if (arr_FirstOrderItem[6] != null)
                    $('#rqty').val(arr_FirstOrderItem[6]);
                if (arr_FirstOrderItem[7] != null)
                    $('#cqty').val(arr_FirstOrderItem[7]);

                //Update the Price for the First Row
                update_price_firstrow();

                //For the Susquent Elements 
                for (j = 1; j < arr_OrderItems.length; j++) {
                    if (arr_OrderItems[j]) {

                        var arr_OrderItem = arr_OrderItems[j].split(",");
                        var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm" ><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td>  <td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                        if (canCall) {
                            $row = $('<tr class="item-row"><td class="tbl-auto-row-01 addproduct"><a class="changeproduct" title="Change Product"> <img src="Images/modifyProduct.jpg" width="16" height="16" /> </a> </td><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm" ><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td>  <td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                        }

                        $row.insertAfter(".item-row:last")
                        $row.find('#ItemDesc').val(arr_OrderItem[0]);
                        $row.find('#suppliercode').val(arr_OrderItem[3]);
                        $row.find('#COG').val(parseFloat(arr_OrderItem[2]).toFixed(2));
                        $row.find('#qty').val(arr_OrderItem[4]);
                        $row.find('#UnitPrice').val(parseFloat(arr_OrderItem[1]).toFixed(2));
                        $row.find('#hidden_item_code').val();
                        $row.find('#hidden_Supplier_Name').val(arr_OrderItem[5]);
                        //Modified here Add Supplier Name instead of Original/Compatible
                        $row.find('#ItemType').val(arr_OrderItem[5]);

                        if (arr_OrderItem[6] != null)
                            $row.find('#rqty').val(arr_OrderItem[6]);
                        if (arr_OrderItem[7] != null)
                            $row.find('#cqty').val(arr_OrderItem[7]);
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


                /*NOT IN USE*/
                //$('#deldet').val(arr_CusDelCost[0]);
                //$('#delcost').val(arr_CusDelCost[1]);

                //Set Promotional Cost Items

                var arr_ProitemCost = ProItems.split("|");

                //Populate Promotional Items  
                if ($('#<%=hdnEditproitpems.ClientID %>').val()) {
                    //populateProItems($('#<%=hdnEditproitpems.ClientID %>').val());
                }

                //Supplier Delivery Cost Population
                arr_SuppDelCost = SuppDelCostItems.split("|");
                //For the Fist Row
                var arr_SuppDelCost_FirstRow = arr_SuppDelCost[0].split(",");


                /*Supplier Delivery Not In USE*/
                //$('#suppdeldet').val(arr_SuppDelCost_FirstRow[0]);
                //$('#suppdelcost').val(arr_SuppDelCost_FirstRow[1]);

                //For SubSquent SuppDelCost Rows 
                for (k = 1; k < arr_SuppDelCost.length; k++) {
                    if (arr_SuppDelCost[k]) {

                        var arr_SuppDelCost_Row = arr_SuppDelCost[k].split(',');
                        var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" disabled="disabled" type="text" class="tbx_cust_delivery" id="suppdeldet"></td><td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td><td width="76" align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td><td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td><td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td></tr>').insertAfter(".supp-del-row:last");

                        /*NOT IN USE*/
                        //$SuppDelCost_Row.find('#suppdeldet').val(arr_SuppDelCost_Row[1]);
                        //$SuppDelCost_Row.find('#suppdelcost').val(arr_SuppDelCost_Row[2]);



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

                    myData.push($row.find('input[name*="rqty"]').val());
                    myData.push($row.find('input[name*="cqty"]').val());
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

                var rowCount = $('#tblLineItems tr').length;
                if (rowCount == 0) {
                    var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td> <td class="tbl-auto-row-10" style="text-align: center;"> <label for="availableQty"></label><span id="availableQty" style="align-content: center;"></span> </td>   <td class="tbl-auto-row-10-btm"><span id="bestprice"></span></td><td class="tbl-auto-row-10-btm"><span id="cartfaulty"></span></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td>     <td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                    if (canCall) {
                        $row = $('<tr class="item-row"><td class="tbl-auto-row-01 addproduct"><a class="changeproduct" title="Change Product"> <img src="Images/modifyProduct.jpg" width="16" height="16" /> </a> </td><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td> <td class="tbl-auto-row-10" style="text-align: center;"> <label for="availableQty"></label><span id="availableQty" style="align-content: center;"></span> </td>   <td class="tbl-auto-row-10-btm"><span id="bestprice"></span></td><td class="tbl-auto-row-10-btm"><span id="cartfaulty"></span></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td>     <td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                    }
                    $('#tblLineItems').append($row);

                    bind($row);
                }

                if (!($('#ItemDesc').val() == '')) {
                    var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                    if (canCall) {
                        $row = $('<tr class="item-row"><td class="tbl-auto-row-01 addproduct"><a class="changeproduct" title="Change Product"> <img src="Images/modifyProduct.jpg" width="16" height="16" /> </a> </td><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-06-btm"><label for="rqty"></label><input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td><td class="tbl-auto-row-06-btm"><label for="cqty"></label><input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>');
                    }
                    $row.insertAfter(".item-row:last");
                    bind($row);
                }
                else {
                    if (rowCount != 0)
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



            ///CREDIT NOTE TOTAL CALCULATIONS
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

                //alert(lineprofittotal);

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


                // alert(promoItemCost);

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

                //alert(COGSubTotalExGST);

                $('#gst').html("$" + parseFloat(COGTotalGSTAmount).toFixed(2));//GST COG 
                $('#fulltotal').html("$" + parseFloat(TotalCOGINCGST).toFixed(2));

                //// Commission Recalculation based on Ex GST values
                ///Commission_Deduction = ((parseFloat(TotalProfitINCGST).toFixed(2) - parseFloat(TotalCOGINCGST).toFixed(2)) * 0.4).toFixed(2);

                thedeltotal = $('#deltotal').html().replace("$", "");

                ordertotal = parseFloat(thedeltotal) + fulltotal;//ORDER COG Total
                $('#ordertotal').html("$" + parseFloat(ordertotal).toFixed(2));//Ordet COG Total


                //Find Rep Commission
                var repCommission = 0;

                if ($('#<%=hdnCommishSplit.ClientID%>').val() == "0") {

                    $('#TR_SalespersonCommission').hide();
                    $.ajax({
                        url: 'fetch/getoperatorcommission.aspx',
                        async: false,
                        data: {
                            repid: $('#<%=ACCOUNT_OWNER_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;

                            var CreditNoteID = $('#<%=hdnCreditNoteID.ClientID%>').val();

                            console.log(CreditNoteID);
                            var CommishPerc = repCommission / 100;
                            
                            //alert(CommishPerc);
                            var Commission = (parseFloat(exgsttotal) * CommishPerc).toFixed(2);
                           // if (CreditNoteID == 1653)
                             //   Commission = 16.37;
                            //if (CreditNoteID == "1219")
                            //    Commission = 5.01;

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
                    $('#TR_SalespersonCommission').show();
                    //Split profits into two
                    var SplitProfits = exgsttotal / 2;
                    var SplitVolume = parseFloat(ProfitSubTotalExGST).toFixed(2) / 2;
                    $('#<%=VOLUME_SPLIT_AMOUNT.ClientID%>').val(SplitVolume);

                    //Split profits into two
                    var SplitProfits = exgsttotal / 2;

                    //Get Commission For Account Owner
                    $.ajax({
                        url: 'fetch/getoperatorcommission.aspx',
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
                       url: 'fetch/getoperatorcommission.aspx',
                       async: false,
                       data: {
                           repid: $('#<%=SALESPERSON_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;
                            
                            var CommishPerc = repCommission / 100;
                            var Commission = (parseFloat(SplitProfits) * CommishPerc).toFixed(2);
                            $('#CD-salespersonname').html($('#<%=SALESPERSON.ClientID%>').val().toUpperCase() + " - ");
                            $('#totalprofitsalesperson').html("$" + parseFloat(Commission).toFixed(2))//Comission 
                            $('#<%=SALESPERSON_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                        },
                        error: function (message) {
                            alert('Unable to retrieve commission for logged user. Please contact your administartor');
                        }
                    });
            }

        };
        });
    //End Calculating Order Total

    </script>

</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManagerupdateCredit" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div class="blackout">
    </div>
    <table align="center" cellpadding="0" cellspacing="0" class="btm_01">
        <tr>
            <td height="25px">
                <asp:Button OnClick="btnaccountDash_Click" ID="Buttonaccount" Text="Account" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />

                <asp:Button OnClick="btnDash_Click" ID="Button1" Text="Dashboard" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moverigRight" CausesValidation="false" />

                 <asp:Button OnClick="btnback_Click" ID="backButton" Text="BACK" ForeColor="Blue" Width="10%" Visible="false"
                    runat="server" CssClass="buttonClass moverigRight" CausesValidation="false" />

            </td>
        </tr>
        <tr>
            <td>

                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td id="OrderTitle" runat="server" class="all-headings-style" style="display: none;">NEW ORDER</td>
                        <td id="QorN" runat="server">&nbsp;</td>
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
                                                            <td class="company-name-style" height="30px">
                                                                <div id="CompanyNameDIV" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="ContactInfo" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="StreetAddressLine1" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="StreetAddressLine2" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-tel-style">
                                                                <div id="ContactandEmail" runat="server"></div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td class="">
                                                    <table align="center" cellpadding="0" cellspacing="0" class="auto-style4" style="display: none;">
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
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryContact" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryCompany" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryAddressLine1" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryAddressLine2" runat="server"></div>
                                                                        </td>
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
                                                <td class="top-payment-terms-heading" style="display: none;">&nbsp;&nbsp;&nbsp;&nbsp;PAYMENT TERMS</td>
                                            </tr>
                                            <tr>
                                                <td style="display: none;">
                                                    <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="payment-terms-drop">
                                                        <asp:ListItem Text="7 days" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="14 days" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="21 days" Value="21" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="30 days" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="days 45" Value="45"></asp:ListItem>

                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px"><b>TYPE OF CREDIT</b></td>
                                            </tr>
                                            <tr>
                                                <td height="16px">
                                                    <input type="text" id="RefID" name="RefID" value="" runat="server" disabled="disabled" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">TYPE OF CALL</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">
                                                    <asp:DropDownList ID="dllTypeOfCall" runat="server">
                                                        <asp:ListItem>Email Marketing</asp:ListItem>
                                                        <asp:ListItem Selected="True">Reorder</asp:ListItem>
                                                        <asp:ListItem>Call In</asp:ListItem>
                                                        <asp:ListItem>Cold Call</asp:ListItem>
                                                        <asp:ListItem>Referral</asp:ListItem>
                                                        <asp:ListItem>Website</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">INVOICE DATE</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">
                                                    <input name="datereceived" type="date" class="FormDateValues" id="datereceived" value="" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" class="top-payment-terms-heading"><b>ORDER NUMBER</b></td>
                                            </tr>
                                            <tr>
                                                <td height="16px">
                                                    <div id="dtsnumber" runat="server"></div>
                                                </td>
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
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="top-payment-terms-heading"><b>ACCOUNT OWNER</b></td>
                                            </tr>


                                            <tr>
                                                <td height="16px">
                                                    <asp:TextBox ID="AccountOwnertxt" ReadOnly="true" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td id="tdOrderCreateDate" class="top-payment-terms-heading" runat="server" style="display: none;">&nbsp;&nbsp;&nbsp;&nbsp;<b>CREATED DATE</b></td>
                                            </tr>

                                            <tr>
                                                <td height="16px">
                                                    <input name="datecreated" type="date" class="FormDateValues" id="datecreated" disabled="disabled" value="" runat="server" style="display: none;" />
                                                </td>
                                            </tr>


                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                                 
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAddCreditNote" runat="server" OnClick="btnAddCreditNote_Click" Visible="false" class="add-credit-note-btn" Text="ADD CREDIT NOTE " />
                                                    <br />
                                                    <br />

                                                    <asp:Button ID="ButtonCreatePopFaulty" runat="server" Visible="false"
                                                        OnClientClick="return openDialogFaulty();" class="submit-btn" Text="UPDATE FAULTY DETAILS" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="ButtonRequestRMA" runat="server" Visible="false"
                                                        OnClientClick="return callRMAREQUESTEMAIL();" class="submit-btn" Text="RMA REQUEST TO SUPPLIER" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="ButtonChangePrinterAddress" runat="server" Visible="false"
                                                        OnClientClick="return callCustmerPOboxSendEMAIL();" class="submit-btn" Text="EMAIL PO-BOX TO CUSTOMER" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="ButtonUpdateRMA" runat="server"
                                                        OnClientClick="return showRMAWindow();" class="submit-btn" Text="UPDATE / VIEW RMA" />

                                                    <br />
                                                    <br />
                                                    <asp:Button ID="ButtonCustomerSentEmail" runat="server" Visible="false"
                                                        OnClientClick="return callCustmerRMAReceivedEMAIL();" class="submit-btn" Text="EMAIL RMA TO CUSTOMER" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnInvoiceApprove" runat="server" Text="APPROVE CREDIT NOTE" OnClick="btnInvoiceApprove_Click" class="submit-btn" Style="display: none;" />

                                                    <br />
                                                    <br />
                                                    <asp:Button ID="UploadfilesBtn" runat="server" OnClientClick="return openMyCon();" ClientIDMode="Static"
                                                        ForeColor="Red" class="submit-btn" Text=" Drag a File to Upload/ Click here " />

                                                     <br />
                                                    <br />
                                                      <asp:Button ID="ButtonCreate" runat="server"  OnClientClick="return openDialogCs();" class="submit-btn" Text=" Raise CS" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="ConverttoInhousebutton" runat="server" Visible="false"
                                                        OnClientClick="return confirmMeConvert();" class="submit-btn" Text="Add Items To InHouse" />

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
     <div id="loadingiamge" style="display:none;">
         Sending....
        </div>

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
                                    <td class="tbl01-clm01-style addproduct" >+<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm01-style">X<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm02-style">ITEM DESCRIPTION<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm03-style">SUPPLIER NAME<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm04-style">SUPPLIER<br />
                                        CODE</td>
                                    <td class="tbl01-clm05-style">COG
                                        <br />
                                        EX GST</td>
                                    <td class="tbl01-clm06-style">QTY<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm06-style">RQTY<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm06-style">CQTY<br />
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
                            <table id="tblLineItems" width="1100" border="0" align="center" cellpadding="0" cellspacing="0" id="lineitems">
                                <tr class="item-row">
                                     <td class="tbl-auto-row-01 addproduct"><a class="changeproduct" title="Change Product"> 
                                         <img src="Images/modifyProduct.jpg" width="16" height="16" /> </a> </td>
                                    <td class="tbl-auto-row-01"><a class="delete" title="Remove row">
                                        <img src="Images/x.png" width="16" height="16" /></td>
                                    <td class="tbl-auto-row-02">
                                        <label for="ItemDesc"></label>
                                        <input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td>
                                    <td class="tbl-auto-row-03">
                                        <label for="suppliercode">
                                            <input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td>
                                    <td class="tbl-auto-row-04">
                                        <label for="suppliercode"></label>
                                        <input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td>
                                    <td class="tbl-auto-row-05">
                                        <label for="COG"></label>
                                        <input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td>
                                    <td class="tbl-auto-row-06">
                                        <label for="qty"></label>
                                        <input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td>
                                     <td class="tbl-auto-row-06">
                                        <label for="rqty"></label>
                                        <input name="rqty" type="text" class="tbl-auto-row-06-inside" id="rqty"></td>
                                     <td class="tbl-auto-row-06">
                                        <label for="cqty"></label>
                                        <input name="cqty" type="text" class="tbl-auto-row-06-inside" id="cqty"></td>
                                    <td class="tbl-auto-row-07">
                                        <label for="UnitPrice"></label>
                                        <input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td>
                                    <td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td>
                                    <td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span>
                                        <label for="hidden_item_code"></label>
                                        <input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden" />
                                        <input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name" value="" />
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

                            <input name="addnewitem" type="button" class="add-btn" id="addnewitem" value="ADD NEW ITEM" />

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
                        <td class="all-subheadings-style" style="display: none;">Delivery</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>



                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
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


                            <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr class="del-row">

                                    <td class="tbl-delivery-01-outside">
                                        <input name="deldet" type="text" class="tbl-delivery-01-inside" id="deldet" />
                                    </td>
                                    <td class="tbl-delivery-02-outside">
                                        <input name="delcost" type="text" class="tbl-delivery-02-inside" id="delcost" />
                                    </td>
                                    <td class="tbl-delivery-03-outside">N/A<input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden" />
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

                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
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
                            <table id="tblSupplierDeliveryCost" width="1000" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr class="supp-del-row">
                                    <td class="tbl-delivery-04-outside">
                                        <input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="suppdeldet" disabled="disabled"></td>
                                    <td class="tbl-delivery-05-outside">
                                        <input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden" /><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" size="1" hidden="hidden" />N/A</td>
                                    <td class="tbl-delivery-06-outside">
                                        <input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost" /></td>
                                </tr>

                            </table>
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
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style" style="display: none;">
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

                                    <td class="tbl04-promo03-style">ITEM CODE<br />
                                        &nbsp;</td>

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

                            <table id="tblProItems" width="940" border="0" align="center" cellpadding="0" cellspacing="0">

                                <tr class="promo-row1">
                                    <td class="tbl04-promo-00-outside">&nbsp;</td>
                                    <td class="tbl04-promo-01-outside">
                                        <input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem" />
                                        <input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden" />
                                    </td>
                                    <td class="tbl04-promo-03-outside">
                                        <input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td>
                                    <td class="tbl04-promo-02-outside">
                                        <input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty" /></td>
                                    <td class="tbl04-promo-03-outside">
                                        <input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>
                                    <td class="tbl04-promo-04-outside">
                                        <input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost" /></td>
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
                                <td class="tbl-totals-02">
                                    <div id="ProfitExGST"></div>
                                </td>
                                <!--Profit Total-->
                                <td class="tbl-totals-03">
                                    <div id="subtotal">$00.00</div>
                                </td>
                                <!--COG Total-->
                            </tr>
                            <tr>
                                <td class="tbl-totals-04">GST</td>
                                <td class="tbl-totals-05">
                                    <div id="profitGST"></div>
                                </td>
                                <td class="tbl-totals-06">
                                    <div id="gst">$00.00</div>
                                </td>
                            </tr>
                            <tr hidden="true">
                                <td width="760" bgcolor="#CCCCCC">Delivery Total</td>
                                <td bgcolor="#CCCCCC">&nbsp;</td>
                                <td bgcolor="#CCCCCC">
                                    <div id="deltotal">$00.00</div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style6">TOTAL INC GST</td>
                                <td class="auto-style7">
                                    <div id="profitFullTotal"></div>
                                </td>
                                <!--Profit Sub Total-->
                                <td class="auto-style8">
                                    <div id="fulltotal">$00.00</div>
                                </td>
                                <!--COG sub Total-->
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
        <td height="20px">&nbsp;</td>
    </tr>

    <tr>
        <td>
            <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">

                <tr>
                    <td width="748" class="comm-01-style"><span id="CD-accountownername"></span>COMMISION &nbsp; DEDUCTION</td>
                    <td class="comm-02-style">
                        <div id="totalprofit">$00.00</div>
                    </td>
                </tr>
                <tr id="TR_SalespersonCommission">
                    <td width="748" class="comm-01-style"><span id="CD-salespersonname"></span>COMMISION &nbsp; DEDUCTION</td>
                    <td class="comm-02-style">
                        <div id="totalprofitsalesperson">$00.00</div>
                    </td>
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

    


    <tr id="faultygoodsTr" runat="server" visible="false">
        <td class="white-bg-style">
            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="all-subheadings-style">Supplier Batch ,Model Faulty Details</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>

                        <asp:TextBox ID="rmafaultyDaetils" TextMode="multiline" class="notes-textbox-style" runat="server"></asp:TextBox>

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
            <asp:Button ID="btnOrderSubmit" runat="server" Text="SAVE" OnClick="btnOrderSubmit_Click" class="submit-btn" ClientIDMode="Static" />
            <asp:Button ID="btnGeneratePDF" runat="server" Text="PDF" OnClick="btnGeneratePDF_Click" class="submit-btn" />
            <asp:Button ID="btnCloseOrderWindow" runat="server" Text="CLOSE" CssClass="submit-btn" ClientIDMode="Static" OnClick="btnCloseOrderWindow_Click" />
            <input type="button" id="btnPrint" name="btnPrint" value="PRINT" class="submit-btn" />
            <input type="button" id="btnCancelInvoice" name="btnCancelInvoice" value="CANCEL  CREDIT NOTE" class="submit-btn" runat="server" />
        </td>
    </tr>

    </table>

    <table id="Table1" width="980" border="0" align="center" cellpadding="0" cellspacing="0" runat="server">



        <tr class="item-supprow">
            <td>
                <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>

        <tr hidden="hidden">
            <td height="20px">
                <input type="text" id="testid" runat="server" />
                <input type="text" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" />
                <input type="text" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" />
                <input type="text" id="hdnProCost" name="hdnProCost" runat="server" />
                <input type="text" id="hdnProfit" name="hdnProfit" runat="server" />
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
                <input type="text" runat="server" name="hdnSupplierDelCostItems" id="hdnSupplierDelCostItems" />
                <input type="text" runat="server" name="hdnProItems" id="hdnProItems" />
                <input type="text" runat="server" name="CusDelCostItems" id="CusDelCostItems" />
                <input type="text" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" />
                <input type="text" runat="server" name="EditProItems" id="EditProItems" />
                <input type="text" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" />
                <input type="text" runat="server" name="hdnSupplierNotes" id="hdnSupplierNotes" />
                <input type="text" runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" />
                <input type="text" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" />
                <input type="text" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" />
                <input type="text" runat="server" name="hdnAllSuppliers" id="hdnAllSuppliers" />
                <input type="text" runat="server" name="hdnSuppliersByCrediotId" id="hdnSuppliersByCrediotId" />
                <input type="text" name="hdnAccountOwner" id="hdnAccountOwner" runat="server" />
                <input type="text" name="navigateURL" id="navigateURL" />
                <input type="text" name="printscreenURL" id="printscreenURL" />
                <input type="text" name="hdnORDERID" id="hdnORDERID" runat="server" />
                <input type="text" name="hdnCommision" id="hdnCommision" runat="server" />
                <input type="text" name="hdnSTATUS" id="hdnSTATUS" runat="server" />
                <input type="text" name="NOTES" id="NOTES" runat="server" />
                <input type="text" name="TempID" id="TempID" runat="server" />
                <input type="text" name="ORDER_STATUS" id="ORDER_STATUS" runat="server" />
                <input type="text" name="ORDER_DATE" id="ORDER_DATE" runat="server" />
                <input type="text" name="ORDER_CREATE_DATE" id="ORDER_CREATE_DATE" runat="server" />

                <input type="text" name="hdnCreditNoteID" id="hdnCreditNoteID" runat="server" />
                CommishSplit
                    <input type="text" name="hdnCommishSplit" id="hdnCommishSplit" runat="server" />
                AccountOwnerID
                    <input type="text" name="ACCOUNT_OWNER_ID" id="ACCOUNT_OWNER_ID" runat="server" />
                AccountOwner
                    <input type="text" name="ACCOUNT_OWNER" id="ACCOUNT_OWNER" runat="server" />
                SalespersonID
                    <input type="text" name="SALESPERSON_ID" id="SALESPERSON_ID" runat="server" />
                Salesperson
                    <input type="text" name="SALESPERSON" id="SALESPERSON" runat="server" />
                AccountOwnerCommission
                    <input type="text" name="ACCOUNT_OWNER_COMMISH" id="ACCOUNT_OWNER_COMMISH" runat="server" />
                SalespersonCommission
                    <input type="text" name="SALESPERSON_COMMISH" id="SALESPERSON_COMMISH" runat="server" />
                SplitVolumeAmount
                    <input type="text" name="VOLUME_SPLIT_AMOUNT" id="VOLUME_SPLIT_AMOUNT" runat="server" />

            </td>

        </tr>

    </table>


    <div id="Dialog-Submit-Confirmation" title="SubmitConfirmation" style="display: none;">

        <table>
            <tr>
                <td>
                    <span id="SubmitMessage"></span>
                </td>
            </tr>

            <tr>

                <td>
                    <input type="button" id="btnReturnDashBoard" value="RETURN TO CREDIT NOTES" /></td>
                <td>
                    <input type="button" id="btnPrintOrder" name="btnPrintOrder" value="PRINT" style="display: none;" /></td>

            </tr>

        </table>

    </div>



    <div id="Dialog-DeleteConfirmation" style="display: none;">

        <table>

            <tr>
                <td>
                    <span id="cancelMessage"><b>YOU ARE ABOUT TO CANCEL THIS INVOICE. DO YOU WISH TO CONTINUE?</b></span>
                </td>
            </tr>

            <tr>
                <td>
                    <input type="button" id="btnYES" name="btnYES" value="YES" /></td>
                <td>
                    <input type="button" id="btnNO" name="btnNO" value="NO" /></td>

            </tr>

        </table>
    </div>

    <div id="Dialog-SuppNotes" style="display: none;">
        <table>
            <tr>
                <td>
                    <input type="text" id="spSupplierTitle" name="spSupplierTitle" disabled="disabled" /></td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <textarea id="taSuppNote" name="taSuppNote"></textarea></td>
            </tr>

            <tr>
                <td>
                    <input type="button" id="submitNote" name="submitNote" value="SUBMIT" /></td>
                <td>
                    <input type="button" id="cancelNote" name="cancelNote" value="CANCEL" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_CloseOrderWindow" style="display: none;">

        <table>
            <tr>
                <td>ARE YOU SURE YOU WANT TO CLOSE THIS ORDER?</td>
            </tr>
            <tr>
                <td>
                    <input type="button" id="btn_CloseWindow_YES" name="btn_CloseWindow_YES" value="YES" /></td>
                <td>
                    <input type="button" id="btn_CloseWindow_NO" name="btn_CloseWindow_NO" value="NO" /></td>



            </tr>

        </table>
    </div>


    <div id="DialogCreditType" style="display: none;">

        <table>
            <tr>

                <td>TYPE OF CREDIT</td>
            </tr>

            <tr>
                <td></td>
            </tr>


        </table>

    </div>

    <div id="DialogCancelCreditNote" style="display: none;">
        <table>
            <tr>

                <td>ARE YOU SURE YOU WANT TO CANCEL THE CREDIT NOTE?</td>
            </tr>

            <tr>
                <td>
                    <input type="button" id="btnCreditYES" value="YES" />
                    <input type="button" id="btnCreditNO" value="NO" />
                </td>
            </tr>

        </table>

    </div>

    <div id="addDialogCreditUpdateFaulty" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Update Credit Note">

        <div id="iframeupdatefaulty"></div>
    </div>

    <div id="addfileforOrder" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Upload Files">



        <%-- <tr>
                <td class="alignRight">OUTCOME :</td>
                <td class="alignLeft">
                    <textarea id="outcomeMessage" cols="70" rows="8"></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">QUESTION :</td>
                <td class="alignLeft">
                    <textarea id="questionMessage" cols="70" rows="8"></textarea></td>
            </tr>--%>

        <div id="iframeHolder"></div>

    </div>

    <div id="rmaRequestSuppName" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Send RMA Request To Supplier">



        <table class="style1">

            <tr>
                <td class="alignRight" style="width: 40%; font-size: 16px">Supplier Name:</td>
                <td class="alignLeft">
                    <asp:DropDownList ID="suppliernameDropRMAEmail" ClientIDMode="Static" runat="server" Width="200px" Height="30px"></asp:DropDownList>
                    <br />
                </td>
            </tr>
        </table>

    </div>

    <div id="rmaPobxCustomer" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Send PoBox To Customer">



        <table class="style1">
            <tr>
                <td class="alignRight"></td>
                <td class="alignLeft" style="font-size: medium;"><strong>ARE YOU SURE YOU WANT TO SEND? </strong></td>

            </tr>
            <tr style="display: none;">
                <td class="alignRight" style="width: 40%; font-size: 16px">Supplier Name:</td>
                <td class="alignLeft">
                    <asp:DropDownList ID="rmaPobxCustomerDrop" ClientIDMode="Static" runat="server" Width="200px" Height="30px"></asp:DropDownList>
                    <br />
                </td>
            </tr>
        </table>

    </div>

    <div id="rmaRequestCustomer" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Send RMA Request To Customer">



        <table class="style1">

            <tr>
                <td class="alignRight" style="width: 40%; font-size: 16px">Supplier Name:</td>
                <td class="alignLeft">
                    <asp:DropDownList ID="suppliernameDropRMAEmailCustomer" ClientIDMode="Static" runat="server" Width="200px" Height="30px"></asp:DropDownList>
                    <br />
                </td>
            </tr>
        </table>

    </div>

    <div id="EditRMA">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <thead>
                <tr style="height: 7px;">
                    <td>&nbsp;</td>
                    <td style="width: 25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Supplier Name:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListRMAEdit" ClientIDMode="Static" runat="server" Width="200px" Height="30px"></asp:DropDownList>
                        <br />
                    </td>

                </tr>


                <tr>
                    <td style="font-size: 16px; text-align: center; width: 150px;">Sent To Supplier</td>

                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 120px;">RMA has been approved</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center;">Credited in Xero</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center;">In House</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; width: 150px;">RMA Sent to Customer</td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 190px;">Adjusted Note Received From Supplier</td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 190px;">Completed</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_S2S" style="vertical-align: middle; display: inline;" /><label for="chk_S2S" /></div>
                    </td>

                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_AR" style="vertical-align: middle;" /><label for="chk_AR" /></div>
                    </td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 25px;">
                            <input type="checkbox" id="chk_CIX" style="vertical-align: middle;" /><label for="chk_CIX" /></div>
                    </td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_InH" style="vertical-align: middle;" /><label for="chk_InH" /></div>
                    </td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_R2C" style="vertical-align: middle;" /><label for="chk_R2C" /></div>
                    </td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 55px;">
                            <input type="checkbox" id="chk_ANFS" style="vertical-align: middle;" /><label for="chk_ANFS" /></div>
                    </td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 55px;">
                            <input type="checkbox" id="chk_Completed" style="vertical-align: middle;" /><label for="chk_Completed" /></div>
                    </td>
                </tr>

            </tbody>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width: 1000px;" align="center">
            <tr>
                <td>&nbsp;</td>
                <td>
                    <input type="text" id="CreditNoteID" hidden="hidden" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 250px;">&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Supplier RMA Number</td>
                <td style="width: 250px;">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">
                    <input type="text" id="RMANumber" name="RMANumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Tracking Number</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">
                    <input type="text" id="TrackingNumber" name="TrackingNumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Notes</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">​<textarea id="rmaNotes" name="rmaNotes" rows="10" cols="70"></textarea>
                </td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Notes History</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px;">​<div id="displayDiv" 
                    runat="server" style=" border:1px solid gray; font: medium -moz-fixed; font: -webkit-small-control;
    height: 120px;
    overflow: auto;
    padding: 2px;
    resize: both;
    width: 500px;word-wrap:break-word;display:inline-block;"></div>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width: 1000px;" align="center">
            <tr>
                <td style="text-align: center;">
                    <input type="button" id="btn_SaveRMAChanges" value="UPDATE" class="btn" /></td>
            </tr>
        </table>

        <br />
        <br />


         <div id="addDialogCs" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Add Customer Service">
        <table class="style1">

            <tr>
                <td class="alignRight">ISSUE:</td>
                <td class="alignLeft">
                    <textarea id="complaintMessage" cols="70" rows="8"></textarea><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">OUTCOME :</td>
                <td class="alignLeft">
                    <textarea id="outcomeMessage" cols="70" rows="8"></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">QUESTION :</td>
                <td class="alignLeft">
                    <textarea id="questionMessage" cols="70" rows="8"></textarea></td>
            </tr>

        </table>

    </div>
    </div>

    <div id="CreateAddWindow" style="display: none">
        <iframe id="createiframe" width="900" height="700" src="Warehouse/AddNewItemReturn.aspx" style="border: 10px;"></iframe>
    </div>

     
   
    <script type="text/javascript">
        var selectedShelId = "";
        function Edit(productCode, productDesc, CogControl, qty, unitPrice) {
            selectedShelId = "";
            //$('#createiframe').attr('src', 'Warehouse/AddNewItem.aspx');
            ViewEditDialog = $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT ITEM',
                width: 900,
            });
            
            ViewEditDialog.dialog("open");
            setEditValue();
            return false;
        }
        var productCode="";
        var productDesc = "";
        var CogControl = "";
        var qty = "";
        var unitPrice = "";

        function setEditValue() {

            $('#createiframe').load(function () {
                $('#createiframe').contents().find('#NewItemCode').val("DS" + productCode);
                $('#createiframe').contents().find('#warehouseitemtrayname').val(productCode);
                $('#createiframe').contents().find('#NewDescription').val(productDesc);
                $('#createiframe').contents().find('#NewCOG').val(CogControl);
                $('#createiframe').contents().find('#Quantity').val(qty);
                $('#createiframe').contents().find('#NewResellPrice').val(unitPrice);

                
            });
           
        }

        function closeAddFrame() {
            ViewEditDialog.dialog("close");
            //  var cogValue = $('#editiframe').contents().find('NewCOG').val();


        }

        $(document).on('click', '.changeproduct', function () {
            // tr class is addproduct
            var $tr = $(this).closest('.item-row');
             productCode = $tr.find('input[name="suppliercode"]').val();
            var suppname = $tr.find('input[name="ItemType"]').val();
             productDesc = $tr.find('input[name="ItemDesc"]').val();
             CogControl = $tr.find('input[name="COG"]').val();
             qty = $tr.find('input[name="qty"]').val();
             unitPrice = $tr.find('input[name="UnitPrice"]').val();
           // console.log("a" + productCode + " " + suppname + " " + CogControl + " " + qty + " " + unitPrice);
            Edit(productCode, productDesc, CogControl, qty, unitPrice);
           
            

        });


        function confirmMeConvert() {

            var result = confirm("Do you want to continue?");
            if (result) {
                var orderCreditidstring = getParameterByName("CreditNoteID");
                PageMethods.UpdateProductINHouse(orderCreditidstring, onSucceedConvert, onConvertError);
                return false;
            }
            else
                return false;
        }

        function InitRaiseWindow() {

            $('#addDialogCs').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "Add",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());


                    var orderIdQstring = getParameterByName("CreditNoteID");
                    var cidQString = getParameterByName("cid");
                    var comidQString = getParameterByName("Compid");

                    var RaiseSalesCsUI = {
                        Complaint: $("#complaintMessage").val(),
                        OutCome: $("#outcomeMessage").val(),
                        Question: $("#questionMessage").val(),
                        orderId: orderIdQstring,
                        contactId: cidQString,
                        companyId: comidQString

                    };

                    if (validateRaiseCsInput()) {
                        //alert("sending " + eventToAdd.title);

                        PageMethods.AddNewRepCS(RaiseSalesCsUI, addSuccess);
                        $(this).dialog("close");
                    }
                }
            }
                ]
            });

        }

        function validateRaiseCsInput() {

            if ($("#complaintMessage").val() == "") {
                alert("Please enter Issue");
                return false;
            }
            //if ($("#outcomeMessage").val() == "") {
            //    alert("Please enter outcome");
            //    return false;
            //}



            return true;
        }


        function openDialogCs() {

            $('#addDialogCs').dialog('open');
            return false;
        }


        function onSucceedConvert(res) {

            alert("Successfully Created.");
            location.reload();
        }

        function onConvertError(res) {
            alert("An Error Occurred.");

        }

        function openMyCon() {
            $('#addfileforOrder').dialog('open');
            var orderIdQstring = getParameterByName("Compid");
            var orderCreditidstring = getParameterByName("CreditNoteID");
            var url = "http://delcrm/UploadCreditInfoFile.aspx?ordID=" + orderIdQstring + "&crID=" + orderCreditidstring;
            $('#iframeHolder').html('<iframe id="iframe" src=' + url + ' width="1000" height="700"></iframe>');
            //  console.log($("#masterpageTableNo"));
            // $('#iframeHolder').find("#masterpageTableNo").hide();
            return false;
        }

        function showLoadingImage() {
            $('#loadingiamge').show();
        }

        function hideLoadingImage() {
            $('#loadingiamge').hide();
        }

        function updateRMAAData() {
            $('#btn_SaveRMAChanges').click(function () {
                //$.ajax({
                //    url: '/Process/EditRMA.aspx',
                //    data: {
                //        QID: $('#CreditNoteID').val(),
                //        STS: $('#chk_S2S').is(':checked'),
                //        ARMA: $('#chk_AR').is(':checked'),
                //        CIX: $('#chk_CIX').is(':checked'),
                //        RTC: $('#chk_R2C').is(':checked'),
                //        ANFS: $('#chk_ANFS').is(':checked'),
                //        INHouse: $('#chk_InH').is(':checked'),
                //        SRMAN: $('#RMANumber').val(),
                //        TN: $('#TrackingNumber').val(),
                //    },
                //    success: function (response) {
                //        alert("Successfully");
                //        $('#EditRMA').dialog('close');
                //    }
                //})

                //alert("sending " + eventToAdd.title);
                var firstDropVal = $('#DropDownListRMAEdit').val();
                var orderIdQstring = getParameterByName("CreditNoteID");
                var RMAUpdate = {
                    CreId: orderIdQstring,
                    STS: $('#chk_S2S').is(':checked'),
                    SuppName: firstDropVal,
                    ARMA: $('#chk_AR').is(':checked'),
                    RTC: $('#chk_R2C').is(':checked'),
                    CIX: $('#chk_CIX').is(':checked'),
                    ANFS: $('#chk_ANFS').is(':checked'),
                    INHouse: $('#chk_InH').is(':checked'),
                    SRMAN: $('#RMANumber').val(),
                    TN: $('#TrackingNumber').val(),
                    Notes: $('#rmaNotes').val(),
                    chk_Completed: $('#chk_Completed').is(':checked')


                };
                PageMethods.RMAUpdateMe(RMAUpdate, addRMASuccess);



            });
        }

        function addRMASuccess(res) {
            alert("RMA Successfully Updated.");
            $('#EditRMA').dialog('close');
        }

        function suppNameEditChange() {

            $("#DropDownListRMAEdit").change(function () {

                var firstDropVal = $('#DropDownListRMAEdit').val();
                var orderIdQstring = getParameterByName("CreditNoteID");
                EditRMAData(orderIdQstring, firstDropVal);
            });
        }

        function callCustmerRMAReceivedEMAIL() {

            $('#rmaRequestCustomer').dialog('open');
            return false;
        }

        function callRMAREQUESTEMAIL() {

            $('#rmaRequestSuppName').dialog('open');
            return false;
        }

        function callCustmerPOboxSendEMAIL() {

            $('#rmaPobxCustomer').dialog('open');
            return false;
        }

        function EditRMAData(CID, sName) {
            
            $('#CreditNoteID').val(CID);
            $.ajax({
                url: '/Fetch/FetchRMABy.aspx',
                data: {
                    RID: CID,
                    sName: sName
                },
                success: function (response) {
                    var splitString = response.split('|');
                    if (splitString[0] == '1') {
                        $('#chk_S2S').prop('checked', true);
                    }
                    else {
                        $('#chk_S2S').prop('checked', false);
                    }

                    if (splitString[1] == '1') {
                        $('#chk_AR').prop('checked', true);
                    }
                    else {
                        $('#chk_AR').prop('checked', false);
                    }

                    if (splitString[2] == '1') {
                        $('#chk_CIX').prop('checked', true);
                    }
                    else {
                        $('#chk_CIX').prop('checked', false);
                    }

                    if (splitString[3] == '1') {
                        $('#chk_R2C').prop('checked', true);
                    }
                    else {
                        $('#chk_R2C').prop('checked', false);
                    }

                    if (splitString[4] == '1') {
                        $('#chk_ANFS').prop('checked', true);
                    }
                    else {
                        $('#chk_ANFS').prop('checked', false);
                    }



                    $('#RMANumber').val(splitString[5]);
                    $('#TrackingNumber').val(splitString[6]);

                    if (splitString[7] == '1') {
                        $('#chk_InH').prop('checked', true);
                    }
                    else {
                        $('#chk_InH').prop('checked', false);
                    }
                    if (splitString[8] != "")
                        $('#ContentPlaceHolder1_displayDiv').html(splitString[8]);
                    else
                        $('#ContentPlaceHolder1_displayDiv').text('');
                    if (splitString[9] == "COMPLETED")
                        $('#chk_Completed').prop('checked', true);
                    else
                        $('#chk_Completed').prop('checked', false);
                }
            })

        }

        function format(d) {
            // `d` is the original data object for the row
            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                    '<td>Sent To Supplier Date Time:</td>' +
                    '<td>' + d.SentToSupplierDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Approved RMA Date Time:</td>' +
                    '<td>' + d.ApprovedRMADateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Credit In Xero Date Time:</td>' +
                    '<td>' + d.CreditInXeroDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Sent To Customer Date Time:</td>' +
                    '<td>' + d.RMAToCustomerDateTime + '</td>' +
                '</tr>' +
                 '<tr>' +
                    '<td>Adjustment Notice Date Time:</td>' +
                    '<td>' + d.AdjustedNoteFromSupplierDateTime + '</td>' +
                '</tr>' +
            '</table>';
        }

        function showEditRMAWindow() {

            $('#EditRMA').dialog({
                resizable: false,
                modal: true,
                title: 'EDIT RMA',
                width: 1200,
                autoOpen: false,
            });
        }

        function showRMAWindow() {

            var orderIdQstring = getParameterByName("CreditNoteID");
            console.log(orderIdQstring);
            var suppNameEdit = $("#DropDownListRMAEdit").val();
            EditRMAData(orderIdQstring, suppNameEdit);
            $('#EditRMA').dialog('open');
            return false;

        }


        function InitRMAEMAILSendWindow() {

            $('#rmaRequestSuppName').dialog({
                autoOpen: false,
                width: 600,
                modal: true,
                buttons: [

            {
                text: "CANCEL",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "SEND",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());


                    var orderIdQstring = getParameterByName("CreditNoteID");
                    var cidQString = getParameterByName("cid");
                    var comidQString = getParameterByName("Compid");

                    var nameSupp = $('#suppliernameDropRMAEmail').val();
                    showLoadingImage();
                    PageMethods.CallRMAEmail(orderIdQstring, nameSupp, cidQString, comidQString, addsendEmailSuccess, sendEmailError);
                    $(this).dialog("close");

                }
            }
                ]
            });

        }

        function InitRMAEMAILToCustomerSendWindow() {

            $('#rmaRequestCustomer').dialog({
                autoOpen: false,
                width: 600,
                modal: true,
                buttons: [

            {
                text: "CANCEL",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "SEND",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());


                    var orderIdQstring = getParameterByName("CreditNoteID");
                    var cidQString = getParameterByName("cid");
                    var comidQString = getParameterByName("Compid");

                    var nameSupp = $('#suppliernameDropRMAEmailCustomer').val();
                    showLoadingImage();
                    PageMethods.RMAReceivedEmailTOCustomer(orderIdQstring, nameSupp, cidQString, comidQString, addsendEmailSuccess, sendEmailError);
                    $(this).dialog("close");

                }
            }
                ]
            });

        }

        function InitPOBOXEMAILToCustomerSendWindow() {

            $('#rmaPobxCustomer').dialog({
                autoOpen: false,
                width: 600,
                modal: true,
                buttons: [

            {
                text: "CANCEL",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "SEND",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());


                    var orderIdQstring = getParameterByName("CreditNoteID");
                    var cidQString = getParameterByName("cid");
                    var comidQString = getParameterByName("Compid");

                    var nameSupp = $('#rmaPobxCustomerDrop').val();
                    showLoadingImage();
                    PageMethods.SendPoBoxInfoTOCustomer(orderIdQstring, nameSupp, cidQString, comidQString, addsendEmailSuccess, sendEmailError);
                    $(this).dialog("close");

                }
            }
                ]
            });

        }



        function addsendEmailSuccess(res) {
            hideLoadingImage();
            alert("RMA Request has been sent successfully.");
        }

        function sendEmailError(res) {
            hideLoadingImage();
            alert("Error occurred.");
        }


        function InitCreditUpdateFaultyWindow() {

            $('#addDialogCreditUpdateFaulty').dialog({
                autoOpen: false,
                width: 1000,
                height:750,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "Add",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());


                    var orderIdQstring = getParameterByName("CreditNoteID");
                    var cidQString = getParameterByName("cid");
                    var comidQString = getParameterByName("Compid");

                    var nameSupp = $('iframe[name=iframefaulyGood]').contents().find("#suppliernameDrop").val();
                    var batchnumbervfaulty = $('iframe[name=iframefaulyGood]').contents().find("#batchnumberfaulty").val();
                    var modelnumbervfaulty = $('iframe[name=iframefaulyGood]').contents().find("#modelnumberfaulty").val();
                    var errormessagefaulty = $('iframe[name=iframefaulyGood]').contents().find("#errormessagefaulty").val();
                    var faultyNotes= $('iframe[name=iframefaulyGood]').contents().find("#faultyNotes").val();
                    //  alert(modelnumbervfaulty);
                    //alert("sending " + eventToAdd.title);
                    var CreditNoteRMAFaultyGoods = {
                        CreditNoteId: orderIdQstring,
                        BatchNumber: batchnumbervfaulty,
                        ModelNumber: modelnumbervfaulty,
                        ErrorMessage: errormessagefaulty,
                        FaultyNotes:faultyNotes,
                        contactId: cidQString,
                        companyId: comidQString,
                        SupplierName: nameSupp

                    };
                    PageMethods.UpdateRMAFaultyGoods(CreditNoteRMAFaultyGoods, addfaultySuccess);
                    $(this).dialog("close");

                }
            }
                ]
            });

        }

        function addfaultySuccess(addResult) {

            alert("Faulty Details Successfully Saved.");

            callSavedFaultyData();
        }


        function callSavedFaultyData() {
            var orderIdQstring = getParameterByName("CreditNoteID");
            PageMethods.ReadFalutyData(orderIdQstring, onSucceed, onError);
            return false;
        }

        function onSucceed(response) {
            //  document.getElementById("statusCheck").checked = false;
            $("#ContentPlaceHolder1_rmafaultyDaetils").val(response);

        }
        function onError(err) {

            alert(err);
        }

        function openDialogFaulty() {

            $('#addDialogCreditUpdateFaulty').dialog('open');
            ;
            var orderIdQstring = getParameterByName("Compid");
            var orderCreditidstring = getParameterByName("CreditNoteID");
            var url = "http://delcrm/UpdateCreditFaultyGoods.aspx?ordID=" + orderIdQstring + "&crID=" + orderCreditidstring;

            $('#iframeupdatefaulty').html('<iframe id="iframefaulyGood" name="iframefaulyGood" src=' + url + ' width="1200" height="850"></iframe>');
            return false;
        }

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        $(document).ready(function () {


            InitUploadWindow();
            callDragOver();
            InitCreditUpdateFaultyWindow();
            InitRMAEMAILSendWindow();
            showEditRMAWindow();
            InitRMAEMAILToCustomerSendWindow();
            suppNameEditChange();
            updateRMAAData();
            InitPOBOXEMAILToCustomerSendWindow();
            InitRaiseWindow();

        });
        function addSuccess(addResult) {

            alert("CS Successfully Saved.");
        }

        function InitUploadWindow() {

            $('#addfileforOrder').dialog({
                autoOpen: false,
                width: 1100,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },

                ]
            });

        }

        function callDragOver() {


            var dropZone = document.getElementById('UploadfilesBtn');
            if (dropZone)
                dropZone.addEventListener('dragover', handleDragOverDiv, false);
        }


        function handleDragOverDiv(event) {
            event.stopPropagation();
            event.preventDefault();
            openMyCon();
        }

    </script>

    <style type="text/css">
        .moverigRight {
            float: right;
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 40px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .ui-widget-content.ui-dialog {
            border: 1px solid #000 !important;
        }

        .ui-button {
            background-color: lightblue !important;
        }

        #waitingLoadingMesage {
 
  position: relative;
  background-color: gray; /* for demonstration */
}
.ajax-loader {
position: absolute;
  left: 0px;
  top: 0px;
  width: 100%;
  height: 100%;
  z-index: 9999;
  background: url('Images/loadingimage1.gif') 50% 50% no-repeat rgb(249,249,249);
  background-repeat : no-repeat;
    background-position : center;
}

#loadingiamge {
    position: absolute;
    top: -140px;
    width: 100%;
    height: 100%;
    background: url(Images/loadingimage1.gif) no-repeat center center;
}
    </style>
</asp:Content>

