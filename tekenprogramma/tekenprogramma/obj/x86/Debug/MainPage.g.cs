﻿#pragma checksum "C:\Users\Trinks\DP\tekenprogramma\tekenprogramma\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "03CF0AF4AF1555E920378BFDFC7C1BB9"
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
            case 1: // MainPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).KeyDown += this.KeyPressed;
                }
                break;
            case 2: // MainPage.xaml line 11
                {
                    this.canvasgrid = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 3: // MainPage.xaml line 21
                {
                    this.bg_paint = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 4: // MainPage.xaml line 22
                {
                    this.paintSurface = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerPressed += this.mouseDown;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerMoved += this.mouseMove;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerReleased += this.mouseUp;
                    ((global::Windows.UI.Xaml.Controls.Canvas)this.paintSurface).PointerWheelChanged += this.mouseScroll;
                }
                break;
            case 5: // MainPage.xaml line 23
                {
                    this.front_canvas = (global::Windows.UI.Xaml.Controls.Canvas)(target);
                }
                break;
            case 6: // MainPage.xaml line 24
                {
                    this.Undo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Undo).Click += this.Undo_Click;
                }
                break;
            case 7: // MainPage.xaml line 29
                {
                    this.Redo = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Redo).Click += this.Redo_Click;
                }
                break;
            case 8: // MainPage.xaml line 34
                {
                    this.Save = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Save).Click += this.Save_Click;
                }
                break;
            case 9: // MainPage.xaml line 39
                {
                    this.Load = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Load).Click += this.Load_Click;
                }
                break;
            case 10: // MainPage.xaml line 44
                {
                    this.Select = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Select).Click += this.Select_Click;
                }
                break;
            case 11: // MainPage.xaml line 49
                {
                    this.Resize = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Resize).Click += this.Resize_Click;
                }
                break;
            case 12: // MainPage.xaml line 54
                {
                    this.Move = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Move).Click += this.Move_Click;
                }
                break;
            case 13: // MainPage.xaml line 59
                {
                    this.Ellipse = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Ellipse).Click += this.Ellipse_Click;
                }
                break;
            case 14: // MainPage.xaml line 64
                {
                    this.Rectangle = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Rectangle).Click += this.Rectangle_Click;
                }
                break;
            case 15: // MainPage.xaml line 69
                {
                    this.Ornament = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Ornament).Click += this.Ornament_Click;
                }
                break;
            case 16: // MainPage.xaml line 70
                {
                    this.Group = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.Group).Click += this.Group_Click;
                }
                break;
            case 17: // MainPage.xaml line 71
                {
                    this.RWidth = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 18: // MainPage.xaml line 72
                {
                    this.RHeight = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 19: // MainPage.xaml line 73
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

