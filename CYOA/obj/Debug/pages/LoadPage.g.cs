﻿#pragma checksum "..\..\..\pages\LoadPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0FB5D06F0BE493207DBC01891F4C449D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CYOA {
    
    
    /// <summary>
    /// LoadPage
    /// </summary>
    public partial class LoadPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 620 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spUpperLeft;
        
        #line default
        #line hidden
        
        
        #line 624 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvAvailableSaves;
        
        #line default
        #line hidden
        
        
        #line 629 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spUpperRight;
        
        #line default
        #line hidden
        
        
        #line 658 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTitle;
        
        #line default
        #line hidden
        
        
        #line 659 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtAuthor;
        
        #line default
        #line hidden
        
        
        #line 660 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSaveDate;
        
        #line default
        #line hidden
        
        
        #line 661 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtCharacter;
        
        #line default
        #line hidden
        
        
        #line 662 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtDecisionsMade;
        
        #line default
        #line hidden
        
        
        #line 663 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtStoryLength;
        
        #line default
        #line hidden
        
        
        #line 669 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spLower;
        
        #line default
        #line hidden
        
        
        #line 678 "..\..\..\pages\LoadPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdChoiceZero;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CYOA;component/pages/loadpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pages\LoadPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.spUpperLeft = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 2:
            this.lvAvailableSaves = ((System.Windows.Controls.ListView)(target));
            
            #line 624 "..\..\..\pages\LoadPage.xaml"
            this.lvAvailableSaves.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvAvailableSaves_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.spUpperRight = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 4:
            this.txtTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtAuthor = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.txtSaveDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtCharacter = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.txtDecisionsMade = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.txtStoryLength = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.spLower = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 11:
            this.brdChoiceZero = ((System.Windows.Controls.Border)(target));
            
            #line 678 "..\..\..\pages\LoadPage.xaml"
            this.brdChoiceZero.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MenuChoice_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

