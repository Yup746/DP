﻿#pragma checksum "C:\Users\Trinks\DP\tekenprogramma\tekenprogramma\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FB3BFDB531B0C0B01CAB420CB262178E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tekenprogramma
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 11
                {
                    this.canvasgrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // MainPage.xaml line 12
                {
                    this.paintSurface = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerPressed += this.mouseDown;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerMoved += this.mouseMove;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerReleased += this.mouseUp;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerWheelChanged += this.mouseScroll;
                }
                break;
            case 4: // MainPage.xaml line 17
                {
                    this.front_canvas = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 5: // MainPage.xaml line 18
                {
                    this.Undo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Undo).Click += this.Undo_Click;
                }
                break;
            case 6: // MainPage.xaml line 19
                {
                    this.Redo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Redo).Click += this.Redo_Click;
                }
                break;
            case 7: // MainPage.xaml line 20
                {
                    this.Save = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Save).Click += this.Save_Click;
                }
                break;
            case 8: // MainPage.xaml line 21
                {
                    this.Load = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Load).Click += this.Load_Click;
                }
                break;
            case 9: // MainPage.xaml line 22
                {
                    this.Select = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Select).Click += this.Select_Click;
                }
                break;
            case 10: // MainPage.xaml line 23
                {
                    this.Resize = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Resize).Click += this.Resize_Click;
                }
                break;
            case 11: // MainPage.xaml line 24
                {
                    this.Move = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Move).Click += this.Move_Click;
                }
                break;
            case 12: // MainPage.xaml line 25
                {
                    this.Ellipse = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Ellipse).Click += this.Ellipse_Click;
                }
                break;
            case 13: // MainPage.xaml line 26
                {
                    this.Rectangle = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Rectangle).Click += this.Rectangle_Click;
                }
                break;
            case 14: // MainPage.xaml line 27
                {
                    this.Ornament = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Ornament).Click += this.Ornament_Click;
                }
                break;
            case 15: // MainPage.xaml line 28
                {
                    this.Group = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Group).Click += this.Group_Click;
                }
                break;
            case 16: // MainPage.xaml line 29
                {
                    this.RWidth = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 17: // MainPage.xaml line 30
                {
                    this.RHeight = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18: // MainPage.xaml line 31
                {
                    this.ROrnament = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
