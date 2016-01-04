﻿#pragma checksum "..\..\..\pages\StorySelection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "08FC20915C26972DC3D6D21362BC0D43"
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
    /// StorySelection
    /// </summary>
    public partial class StorySelection : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 620 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spUpperLeft;
        
        #line default
        #line hidden
        
        
        #line 624 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvAvailableAdventures;
        
        #line default
        #line hidden
        
        
        #line 629 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spUpperRight;
        
        #line default
        #line hidden
        
        
        #line 652 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtTitle;
        
        #line default
        #line hidden
        
        
        #line 653 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtAuthor;
        
        #line default
        #line hidden
        
        
        #line 654 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtPublishDate;
        
        #line default
        #line hidden
        
        
        #line 655 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSummary;
        
        #line default
        #line hidden
        
        
        #line 661 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DockPanel spLower;
        
        #line default
        #line hidden
        
        
        #line 671 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdChoiceZero;
        
        #line default
        #line hidden
        
        
        #line 674 "..\..\..\pages\StorySelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border brdChoiceOne;
        
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
            System.Uri resourceLocater = new System.Uri("/CYOA;component/pages/storyselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\pages\StorySelection.xaml"
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
            this.lvAvailableAdventures = ((System.Windows.Controls.ListView)(target));
            
            #line 624 "..\..\..\pages\StorySelection.xaml"
            this.lvAvailableAdventures.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lvAvailableAdventures_SelectionChanged);
            
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
            this.txtPublishDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtSummary = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.spLower = ((System.Windows.Controls.DockPanel)(target));
            return;
            case 9:
            this.brdChoiceZero = ((System.Windows.Controls.Border)(target));
            
            #line 671 "..\..\..\pages\StorySelection.xaml"
            this.brdChoiceZero.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MenuChoice_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.brdChoiceOne = ((System.Windows.Controls.Border)(target));
            
            #line 674 "..\..\..\pages\StorySelection.xaml"
            this.brdChoiceOne.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MenuChoice_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
