﻿#pragma checksum "D:\Philipp\Software-Engineering\de.efsdev\windowsphone\TankManager\TankManager WP7\Views\InfoView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "26E3D001981FB13D7ADD08EBBC6ADF2B"
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


namespace TankManager.Views {
    
    
    public partial class InfoView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.Border Border_Logo;
        
        internal System.Windows.Controls.TextBlock tblAppTitle;
        
        internal System.Windows.Controls.TextBlock tblVersion;
        
        internal System.Windows.Controls.TextBlock tblLastUpdate;
        
        internal Microsoft.Phone.Controls.Pivot Pivot_Info;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_About;
        
        internal System.Windows.Controls.TextBlock tblAppCompany;
        
        internal System.Windows.Controls.HyperlinkButton hlbtnEmail;
        
        internal System.Windows.Controls.Button Button_RateReview;
        
        internal System.Windows.Controls.TextBlock tblAbout;
        
        internal System.Windows.Controls.TextBlock tblCopyright;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Donation;
        
        internal System.Windows.Controls.Button Button_Donate;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_History;
        
        internal System.Windows.Controls.ListBox ListBox_History;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Credits;
        
        internal System.Windows.Controls.ListBox ListBox_Credits;
        
        internal Microsoft.Phone.Controls.PivotItem PivotItem_Diagnostic;
        
        internal System.Windows.Controls.TextBlock TextBlock_StartTime;
        
        internal System.Windows.Controls.TextBlock TextBlock_CurrentRam;
        
        internal System.Windows.Controls.TextBlock TextBlock_PeakRam;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TankManager%20WP7;component/Views/InfoView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.Border_Logo = ((System.Windows.Controls.Border)(this.FindName("Border_Logo")));
            this.tblAppTitle = ((System.Windows.Controls.TextBlock)(this.FindName("tblAppTitle")));
            this.tblVersion = ((System.Windows.Controls.TextBlock)(this.FindName("tblVersion")));
            this.tblLastUpdate = ((System.Windows.Controls.TextBlock)(this.FindName("tblLastUpdate")));
            this.Pivot_Info = ((Microsoft.Phone.Controls.Pivot)(this.FindName("Pivot_Info")));
            this.PivotItem_About = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_About")));
            this.tblAppCompany = ((System.Windows.Controls.TextBlock)(this.FindName("tblAppCompany")));
            this.hlbtnEmail = ((System.Windows.Controls.HyperlinkButton)(this.FindName("hlbtnEmail")));
            this.Button_RateReview = ((System.Windows.Controls.Button)(this.FindName("Button_RateReview")));
            this.tblAbout = ((System.Windows.Controls.TextBlock)(this.FindName("tblAbout")));
            this.tblCopyright = ((System.Windows.Controls.TextBlock)(this.FindName("tblCopyright")));
            this.PivotItem_Donation = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Donation")));
            this.Button_Donate = ((System.Windows.Controls.Button)(this.FindName("Button_Donate")));
            this.PivotItem_History = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_History")));
            this.ListBox_History = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_History")));
            this.PivotItem_Credits = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Credits")));
            this.ListBox_Credits = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_Credits")));
            this.PivotItem_Diagnostic = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Diagnostic")));
            this.TextBlock_StartTime = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_StartTime")));
            this.TextBlock_CurrentRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_CurrentRam")));
            this.TextBlock_PeakRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_PeakRam")));
            this.TextBlock_MaxRam = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_MaxRam")));
            this.Button_Logs = ((System.Windows.Controls.Button)(this.FindName("Button_Logs")));
            this.PivotItem_Feedback = ((Microsoft.Phone.Controls.PivotItem)(this.FindName("PivotItem_Feedback")));
            this.ListPicker_Feedback = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("ListPicker_Feedback")));
            this.TextBox_Feedback = ((System.Windows.Controls.TextBox)(this.FindName("TextBox_Feedback")));
        }
    }
}
