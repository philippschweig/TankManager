﻿#pragma checksum "D:\Philipp\Software-Engineering\de.efsdev\windowsphone\TankManager\TankManager WP7\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "522CAEA340D53271A690D9DA3CBD9D35"
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
using Microsoft.Phone.Shell;
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


namespace TankManager {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AppBarMenuItem_Help;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AppBarMenuItem_Settings;
        
        internal Microsoft.Phone.Shell.ApplicationBarMenuItem AppBarMenuItem_Infos;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton AppBarIconButton_Tanken;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton AppBarIconButton_FuelLog;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama Panorama_MainPage;
        
        internal Microsoft.Phone.Controls.PanoramaItem PanoramaItem_Overview;
        
        internal System.Windows.Controls.ScrollViewer ScrollViewer_Overview;
        
        internal System.Windows.Controls.TextBlock TextBlock_Kennzeichen;
        
        internal System.Windows.Controls.TextBlock TextBlock_Name;
        
        internal System.Windows.Controls.Button Button_VehicleEdit;
        
        internal System.Windows.Controls.Grid Grid_OverviewNoContent;
        
        internal Microsoft.Phone.Controls.PanoramaItem PanoramaItem_Fuellog;
        
        internal System.Windows.Controls.ListBox ListBox_Fuellog;
        
        internal System.Windows.Controls.Grid Grid_FuellogNoContent;
        
        internal System.Windows.Controls.TextBlock FuellogNoContent_Header;
        
        internal System.Windows.Controls.TextBlock FuellogNoContent_Content;
        
        internal Microsoft.Phone.Controls.PanoramaItem PanoramaItem_DataAnalysis;
        
        internal System.Windows.Controls.Grid Grid_DataAnalysis;
        
        internal System.Windows.Controls.ListBox ListBox_DataAnalysis;
        
        internal Microsoft.Phone.Controls.MenuItem MenuItem_Tanktabelle;
        
        internal System.Windows.Controls.Grid Grid_DataAnalysisNoContent;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TankManager%20WP7;component/MainPage.xaml", System.UriKind.Relative));
            this.AppBarMenuItem_Help = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AppBarMenuItem_Help")));
            this.AppBarMenuItem_Settings = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AppBarMenuItem_Settings")));
            this.AppBarMenuItem_Infos = ((Microsoft.Phone.Shell.ApplicationBarMenuItem)(this.FindName("AppBarMenuItem_Infos")));
            this.AppBarIconButton_Tanken = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("AppBarIconButton_Tanken")));
            this.AppBarIconButton_FuelLog = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("AppBarIconButton_FuelLog")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.Panorama_MainPage = ((Microsoft.Phone.Controls.Panorama)(this.FindName("Panorama_MainPage")));
            this.PanoramaItem_Overview = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("PanoramaItem_Overview")));
            this.ScrollViewer_Overview = ((System.Windows.Controls.ScrollViewer)(this.FindName("ScrollViewer_Overview")));
            this.TextBlock_Kennzeichen = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_Kennzeichen")));
            this.TextBlock_Name = ((System.Windows.Controls.TextBlock)(this.FindName("TextBlock_Name")));
            this.Button_VehicleEdit = ((System.Windows.Controls.Button)(this.FindName("Button_VehicleEdit")));
            this.Grid_OverviewNoContent = ((System.Windows.Controls.Grid)(this.FindName("Grid_OverviewNoContent")));
            this.PanoramaItem_Fuellog = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("PanoramaItem_Fuellog")));
            this.ListBox_Fuellog = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_Fuellog")));
            this.Grid_FuellogNoContent = ((System.Windows.Controls.Grid)(this.FindName("Grid_FuellogNoContent")));
            this.FuellogNoContent_Header = ((System.Windows.Controls.TextBlock)(this.FindName("FuellogNoContent_Header")));
            this.FuellogNoContent_Content = ((System.Windows.Controls.TextBlock)(this.FindName("FuellogNoContent_Content")));
            this.PanoramaItem_DataAnalysis = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("PanoramaItem_DataAnalysis")));
            this.Grid_DataAnalysis = ((System.Windows.Controls.Grid)(this.FindName("Grid_DataAnalysis")));
            this.ListBox_DataAnalysis = ((System.Windows.Controls.ListBox)(this.FindName("ListBox_DataAnalysis")));
            this.MenuItem_Tanktabelle = ((Microsoft.Phone.Controls.MenuItem)(this.FindName("MenuItem_Tanktabelle")));
            this.Grid_DataAnalysisNoContent = ((System.Windows.Controls.Grid)(this.FindName("Grid_DataAnalysisNoContent")));
        }
    }
}

