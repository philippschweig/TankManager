﻿#pragma checksum "D:\Philipp\Software-Engineering\de.efsdev\crossplatform\EFSresources\EFSwp7.wp7\EFSviews\Information\InfoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "90793FD414300941C80016007267489F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34003
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace EFSresources.Views.Information {
    
    
    public partial class InfoView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Border Border_Logo;
        
        internal System.Windows.Controls.Image Image_Logo;
        
        internal System.Windows.Controls.StackPanel StackPanel_AppInfo;
        
        internal System.Windows.Controls.TextBlock tblAppTitle;
        
        internal System.Windows.Controls.TextBlock tblVersionHeader;
        
        internal System.Windows.Controls.TextBlock tblVersion;
        
        internal System.Windows.Controls.TextBlock tblLastUpdateHeader;
        
        internal System.Windows.Controls.TextBlock tblLastUpdate;
        
        internal Microsoft.Phone.Controls.Pivot Pivot_Info;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_About;
        
        internal System.Windows.Controls.TextBlock tblAppCompanyHeader;
        
        internal System.Windows.Controls.TextBlock tblAppCompany;
        
        internal System.Windows.Controls.TextBlock tblEmailHeader;
        
        internal System.Windows.Controls.HyperlinkButton hlbtnEmail;
        
        internal System.Windows.Controls.TextBlock tblSupportHeader;
        
        internal System.Windows.Controls.HyperlinkButton hlbtnSupport;
        
        internal System.Windows.Controls.Button Button_RateReview;
        
        internal System.Windows.Controls.TextBlock tblCopyright;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_History;
        
        internal System.Windows.Controls.ListBox ListBox_History;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Credits;
        
        internal System.Windows.Controls.ListBox ListBox_Credits;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Diagnostic;
        
        internal System.Windows.Controls.TextBlock TextBlock_StartTimeHeader;
        
        internal System.Windows.Controls.TextBlock TextBlock_StartTime;
        
        internal System.Windows.Controls.TextBlock TextBlock_CurrentRamHeader;
        
        internal System.Windows.Controls.TextBlock TextBlock_CurrentRam;
        
        internal System.Windows.Controls.TextBlock TextBlock_PeakRamHeader;
        
        internal System.Windows.Controls.TextBlock TextBlock_PeakRam;
        
        internal System.Windows.Controls.TextBlock TextBlock_MaxRamHeader;
        
        internal System.Windows.Controls.TextBlock TextBlock_MaxRam;
        
        internal System.Windows.Controls.Button Button_Logs;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Feedback;
        
        internal Microsoft.Phone.Controls.ListPicker ListPicker_Feedback;
        
        internal System.Windows.Controls.TextBox TextBox_Feedback;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/EFSviews;component/Information/InfoView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Border_Logo = ((System.Windows.Controls.Border)(this.FindName("Border_Logo")));
            this.Image_Logo = ((System.Windows.Controls.Image)(this.FindName("Image_Logo")));
            this.StackPanel_AppInfo = ((System.Windows.Controls.StackPanel)(this.FindName("StackPanel_AppInfo")));
            this.tblAppTitle = ((System.Windows.Controls.TextBlock)(this.FindName("tblAppTitle")));
            this.tblVersionHeader = ((System.Windows.Controls.TextBlock)(this.FindName("tblVersionHeader")));
            this.tblVersion = ((System.Windows.Controls.TextBlock)(this.FindName("tblVersion")));
            this.tblLastUpdateHeader = ((System.Windows.Controls.TextBlock)(this.FindName("tblLastUpdateHeader")));
            this.tblLastUpdate = ((System.Windows.Controls.TextBlock)(this.FindName("tblLastUpdate")));
            this.Pivot_Info = ((Microsoft.Phone.Controls.Pivot)(this.FindName("Pivot_Info")));
            this.PivotItem_About = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_About")));
            this.tblAppCompanyHeader = ((System.Windows.Controls.TextBlock)(this.FindName("tblAppCompanyHeader")));
            this.tblAppCompany = ((System.Windows.Controls.TextBlock)(this.FindName("tblAppCompany")));
            this.tblEmailHeader = ((System.Windows.Controls.TextBlock)(this.FindName("tblEmailHeader")));
            this.hlbtnEmail = ((System.Windows.Controls.HyperlinkButton)(this.FindName("hlbtnEmail")));
            this.tblSupportHeader = ((System.Windows.Controls.TextBlock)(this.FindName("tblSupportHeader")));
            this.hlbtnSupport = ((System.Windows.Controls.HyperlinkButton)(this.FindName("hlbtnSupport")));
            this.Button_RateReview = ((System.Windows.Controls.Button)(this.FindName("Button_RateReview")));
            this.tblCopyright = ((System.Windows.Controls.TextBlock)(this.FindName("tblCopyright")));
            this.PivotItem_History = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_History")));
            this.ListBox_History = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_History")));
            this.PivotItem_Credits = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Credits")));
            this.ListBox_Credits = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_Credits")));
            this.PivotItem_Diagnostic = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Diagnostic")));
            this.TextBlock_StartTimeHeader = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_StartTimeHeader")));
            this.TextBlock_StartTime = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_StartTime")));
            this.TextBlock_CurrentRamHeader = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_CurrentRamHeader")));
            this.TextBlock_CurrentRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_CurrentRam")));
            this.TextBlock_PeakRamHeader = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_PeakRamHeader")));
            this.TextBlock_PeakRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_PeakRam")));
            this.TextBlock_MaxRamHeader = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_MaxRamHeader")));
            this.TextBlock_MaxRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_MaxRam")));
            this.Button_Logs = ((System.Windows.Controls.Button)(this.FindName("Button_Logs")));
            this.PivotItem_Feedback = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Feedback")));
            this.ListPicker_Feedback = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("ListPicker_Feedback")));
            this.TextBox_Feedback = ((System.Windows.Controls.TextBox)(this.FindName("TextBox_Feedback")));
        }
    }
}

