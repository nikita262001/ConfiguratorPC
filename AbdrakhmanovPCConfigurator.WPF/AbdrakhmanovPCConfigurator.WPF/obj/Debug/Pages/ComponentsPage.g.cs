#pragma checksum "..\..\..\Pages\ComponentsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "857E551BF095473DC5681C3693FF8242DD0C4D22B1190E51CAD1E932CEEB059E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AbdrakhmanovPCConfigurator.WPF.Pages;
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


namespace AbdrakhmanovPCConfigurator.WPF.Pages {
    
    
    /// <summary>
    /// ComponentsPage
    /// </summary>
    public partial class ComponentsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboTypeComponent;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackTree;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonAddParametrs;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackFullParametrs;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textPrice;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stackParametrs;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\ComponentsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button;
        
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
            System.Uri resourceLocater = new System.Uri("/AbdrakhmanovPCConfigurator.WPF;component/pages/componentspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ComponentsPage.xaml"
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
            
            #line 9 "..\..\..\Pages\ComponentsPage.xaml"
            ((AbdrakhmanovPCConfigurator.WPF.Pages.ComponentsPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.comboTypeComponent = ((System.Windows.Controls.ComboBox)(target));
            
            #line 16 "..\..\..\Pages\ComponentsPage.xaml"
            this.comboTypeComponent.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MainComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.stackTree = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.buttonAddParametrs = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Pages\ComponentsPage.xaml"
            this.buttonAddParametrs.Click += new System.Windows.RoutedEventHandler(this.AddParametrs_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.stackFullParametrs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.textPrice = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\..\Pages\ComponentsPage.xaml"
            this.textPrice.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 27 "..\..\..\Pages\ComponentsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddParametr_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.stackParametrs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            
            #line 29 "..\..\..\Pages\ComponentsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectImage_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.button = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\Pages\ComponentsPage.xaml"
            this.button.Click += new System.Windows.RoutedEventHandler(this.SaveWithParametrs_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

