using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Streams;
//using System.Drawing;

namespace tekenprogramma
{
    public partial class MainPage : Page
    {
        //Initial variables
        int lptag;
        public static int itemcount = 0;
        List<int> selected = new List<int>();
        public static int timeindex = 0;
        Composite group = new Composite(0, "Group");
        History history = History.GetInstance();
        string tool;
        List<Button> buttons = new List<Button>();
        StorageFolder savefolder;
        Brush buttoncolor;
        Windows.UI.Input.PointerPoint dragStart = null;
        int anchor = -1;

        public MainPage()
        {
            //Initializing program and Command DP
            InitializeComponent();
            buttoncolor = Move.Background;
            history.timeline.Add(new Snapshot(group.Copy(), itemcount));
            buttons.Add(Move);
            buttons.Add(Ellipse);
            buttons.Add(Rectangle);
            buttons.Add(Select);
            Selectbutton(Rectangle);
        }
        
        //Anything on the paint surface gets pressed, even the drawings already there
        private void mouseDown(object sender, PointerRoutedEventArgs e)
        {
            //If the moving button has been pressed, this part moves the drawing
            if (tool == "Move" && ((FrameworkElement)sender).Name == paintSurface.Name)
            {
                dragStart = e.GetCurrentPoint(paintSurface);
                Update(true);
            }
            //If the group button has been pressed, this part lists all shapes that are being selected
            else if(tool == "Select" && ((FrameworkElement)sender).Name != paintSurface.Name)
            {
                lptag = Convert.ToInt32((e.OriginalSource as FrameworkElement).Tag);
                if (!selected.Contains(lptag))
                    selected.Add(lptag);
                else
                    selected.Remove(lptag);
                if (selected.Count == 1)
                    anchor = selected[0];
                Update(false);
            }
            //This part handles the first and second set of coordinates respectively to draw new shapes
            else if(tool == "Rectangle" || tool == "Ellipse")
            {
                itemcount++;
                dragStart = e.GetCurrentPoint(paintSurface);
                Composite newshape = new Composite(itemcount, tool);
                newshape.height = 1;
                newshape.width = 1;
                newshape.x = e.GetCurrentPoint(paintSurface).Position.X;
                newshape.y = e.GetCurrentPoint(paintSurface).Position.Y;
                group.Add(newshape);
                Update(true);
                Update(false);
            }
        }

        private void mouseUp(object sender, PointerRoutedEventArgs e) {
            if (tool == "Move")
            {
                /*var element = (UIElement)sender;
                element.ReleasePointerCapture(e.Pointer);

                lptag = Convert.ToInt32(((FrameworkElement)sender).Tag);
                Composite newcomposite = group.FindID(lptag).Copy();
                newcomposite.x = e.GetCurrentPoint(paintSurface).Position.X - dragStart.Position.X;
                newcomposite.y = e.GetCurrentPoint(paintSurface).Position.Y - dragStart.Position.Y;
                group.SetID(newcomposite, lptag);
                Update(true);
                Update(false);*/
                dragStart = null;
            }
            /*else if(tool == "Rectangle" || tool == "Ellipse")
            {
                Update(true);
            }*/
        }

        private void mouseMove(object sender, PointerRoutedEventArgs e)
        {
            if (tool == "Move" && dragStart != null && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                timeindex--; ;
                Update(false);
                var p2 = e.GetCurrentPoint(paintSurface);
                foreach(int tag in selected)
                {
                    Composite tmp = group.FindID(tag).Copy();
                    tmp.RMove(p2.Position.X - dragStart.Position.X, p2.Position.Y - dragStart.Position.Y);
                    group.SetID(tmp, tag);
                }
                Update(true);
                Update(false);
                //ROrnament.PlaceholderText = timeindex.ToString();
                //Canvas.SetLeft(element, p2.Position.X - dragStart.Position.X);
                //Canvas.SetTop(element, p2.Position.Y - dragStart.Position.Y);
            }
            else if((tool == "Rectangle" || tool == "Ellipse") && dragStart != null && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                Undo_Click(sender, e);
                itemcount++;
                Composite newshape = new Composite(itemcount, tool);
                newshape.height = Math.Abs(dragStart.Position.Y - e.GetCurrentPoint(paintSurface).Position.Y);
                newshape.width = Math.Abs(dragStart.Position.X - e.GetCurrentPoint(paintSurface).Position.X);
                newshape.x = ReturnSmallest(dragStart.Position.X, e.GetCurrentPoint(paintSurface).Position.X);
                newshape.y = ReturnSmallest(dragStart.Position.Y, e.GetCurrentPoint(paintSurface).Position.Y);
                group.Add(newshape);
                Update(true);
                Update(false);
            }
        }

        private void mouseScroll(object sender, PointerRoutedEventArgs e)
        {
            if (selected.Count == 1)
            {
                if(e.GetCurrentPoint(paintSurface).Properties.MouseWheelDelta >= 0)
                {
                    int tmp = group.Findparent(selected[0]);
                    if(tmp != -1)
                    {
                        selected.Clear();
                        selected.Add(tmp);
                        Update(false);
                    }
                }
                if (e.GetCurrentPoint(paintSurface).Properties.MouseWheelDelta <= 0)
                {
                    if(group.FindID(selected[0]).type == "Group")
                    {
                        foreach (Composite c in group.FindID(selected[0]).groupitems)
                        {
                            Composite x = c.FindID(anchor);
                            if (x.type != "Placeholder")
                            {
                                selected.Clear();
                                selected.Add(c.id);
                                Update(false);
                            }
                        }
                    }
                }
            }
              //  if(e.GetCurrentPoint(paintSurface).Properties.MouseWheelDelta >= 0)
        }

        //This button activates the posibility in Main to move a shape
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Move);
        }

        //This button resizes the last pressed shape according to the entered text in the text boxes
        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            if (RHeight.Text != "" && RWidth.Text != "")
            {
                foreach(int c in selected)
                {
                    Composite newcomposite = group.FindID(c).Copy();
                    newcomposite.RChangeSize(Convert.ToDouble(RHeight.Text), Convert.ToDouble(RWidth.Text));
                    group.SetID(newcomposite, c);
                }
                Update(true);
                Update(false);
            }
        }

        //This part mekes it either possible to select groupable shapes in main or groups them if there's already a selection made
        private void Group_Click(object sender, RoutedEventArgs e)
        {
            if (selected.Count != 0)
            {
                itemcount++;
                group.Add(new Composite(itemcount, "Group"));
                foreach (int c in selected)
                {
                    Composite tmp = group.FindID(c).Copy();
                    group.RemoveID(c);
                    group.groupitems[group.groupitems.Count - 1].Add(tmp);
                }
                selected.Clear();
                anchor = -1;
                Update(true);
                Update(false);
            }
        }

        //This activates the undo command
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            selected.Clear();
            anchor = -1;
            if(timeindex > 0)
            {
                timeindex--;
                Update(false);
            }
        }

        //This activates the redo command pattern
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            selected.Clear();
            anchor = -1;
            if(history.timeline.Count - 1 > timeindex)
            {
                timeindex++;
                Update(false);
            }
        }

        //This finds an element by it's id from the paintsurface and returns it
        public FrameworkElement ElementbyTag(int tag)
        {
            foreach(FrameworkElement c in paintSurface.Children)
            {
                FrameworkElement tmp = c;
                if(Convert.ToInt32(tmp.Tag) == tag)
                {
                    return tmp;
                }
            }
            return paintSurface;
        }

        //This returns the smalles double from the 2 inputs
        public static double ReturnSmallest(double first, double last)
        {
            if(first < last)
            {
                return first;
            }
            else
            {
                return last;
            }
        }


        //This changes the to-be type to an Ellipse
        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Ellipse);
        }

        //This changes the to-be type to a Rectangle
        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Rectangle);
        }

        //Update after timeshift or changes made
        public void Update(bool change)
        {
            //true means that an actual change has been made and needs to be stored in the history
            if (change)
            {
                timeindex++; ;
                if (history.timeline.Count >= timeindex)
                {
                    history.timeline.RemoveRange(timeindex, history.timeline.Count - timeindex);
                }
                history.timeline.Add(new Snapshot(group.Copy(), itemcount));
            }
            else
            {
                //false means that the history needs to be read and written to the paintsurface
                group = history.timeline[timeindex].composite.Copy();
                itemcount = history.timeline[timeindex].itemcount;
                paintSurface.Children.Clear();
                List<Composite> tosurface = history.timeline[timeindex].composite.Concatenate();
                foreach (Composite c in tosurface)
                {
                    if(c.type == "Rectangle")
                    {
                        Rectangle newRectangle = new Rectangle();
                        newRectangle.Height = c.height;
                        newRectangle.Width = c.width;
                        SolidColorBrush brush = new SolidColorBrush();
                        Composite tmp = c;
                        if (selected.Contains(c.id))
                            brush.Color = Colors.DarkSlateGray;
                        else
                        {
                            brush.Color = Colors.SlateGray;
                            while (tmp.id != 0 && selected.Count > 0)
                            {
                                int parent = group.Findparent(tmp.id);
                                if (selected.Contains(parent))
                                {
                                    brush.Color = Colors.DarkSlateGray;
                                    break;
                                }
                                else
                                    tmp = group.FindID(parent);
                            }
                        }
                        newRectangle.Fill = brush;
                        newRectangle.Name = "Rectangle";
                        newRectangle.Tag = c.id;
                        Canvas.SetLeft(newRectangle, c.x);
                        Canvas.SetTop(newRectangle, c.y);
                        newRectangle.PointerPressed += mouseDown;
                        newRectangle.PointerMoved += mouseMove;
                        newRectangle.PointerReleased += mouseUp;
                        newRectangle.PointerWheelChanged += mouseScroll;
                        paintSurface.Children.Add(newRectangle);
                    }
                    else if(c.type == "Ellipse")
                    {
                        Ellipse newEllipse = new Ellipse();
                        newEllipse.Height = c.height;
                        newEllipse.Width = c.width;
                        SolidColorBrush brush = new SolidColorBrush();
                        Composite tmp = c;
                        if (selected.Contains(c.id))
                            brush.Color = Colors.DarkSlateGray;
                        else
                        {
                            brush.Color = Colors.SlateGray;
                            while (tmp.id != 0 && selected.Count > 0)
                            {
                                int parent = group.Findparent(tmp.id);
                                if (selected.Contains(parent))
                                {
                                    brush.Color = Colors.DarkSlateGray;
                                    break;
                                }
                                else
                                    tmp = group.FindID(parent);
                            }
                        }
                        newEllipse.Fill = brush;
                        newEllipse.Name = "Ellipse";
                        newEllipse.Tag = c.id;
                        Canvas.SetLeft(newEllipse, c.x);
                        Canvas.SetTop(newEllipse, c.y);
                        newEllipse.PointerPressed += mouseDown;
                        newEllipse.PointerMoved += mouseMove;
                        newEllipse.PointerReleased += mouseUp;
                        newEllipse.PointerWheelChanged += mouseScroll;
                        paintSurface.Children.Add(newEllipse);
                    }
                }
            }
        }
        
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Select);
        }

        //Doesn't work yet, but I use it to break the program for debugging
        private void Ornament_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //Saves the current shapes and groups to a file
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads;
            picker.FileTypeFilter.Add("*");

            savefolder = await picker.PickSingleFolderAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("savefolder", savefolder);

            StorageFile obsolete = await savefolder.GetFileAsync("save.txt");

            if (obsolete != null)
            {
                await obsolete.DeleteAsync();
            }

            StorageFile savefile = await savefolder.CreateFileAsync("save.txt");
            IRandomAccessStream sw = await savefile.OpenAsync(FileAccessMode.ReadWrite);

            DataWriter writer = new DataWriter(sw);
            group.Savetofile(writer, 0);
            await writer.StoreAsync();
        }

        //Loads the shapes from a file
        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads;
            picker.FileTypeFilter.Add("*");

            savefolder = await picker.PickSingleFolderAsync();
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.AddOrReplace("savefolder", savefolder);

            StorageFile load = await savefolder.GetFileAsync("save.txt");
            IRandomAccessStream sw = await load.OpenAsync(FileAccessMode.Read);
            var istream = sw.GetInputStreamAt(0);
            DataReader reader = new DataReader(istream);
            await reader.LoadAsync(Convert.ToUInt32(sw.Size));
            string loadstring = reader.ReadString(Convert.ToUInt32(sw.Size));
            List<string> lines = new List<string>();
            lines.AddRange(loadstring.Split("\n"));
            lines.RemoveAt(0);
            lines.RemoveAt(lines.Count - 1);
            group.groupitems.Clear();
            itemcount = 1;
            group.Loadfromfile(lines);
            Update(true);
            Update(false);
        }

        private void Selectbutton(Button button)
        {
            foreach (Button b in buttons){
                b.Background = buttoncolor;
            }
            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(255, 100, 100, 100);
            button.Background = color;
            tool = button.Name;
        }
    }
}