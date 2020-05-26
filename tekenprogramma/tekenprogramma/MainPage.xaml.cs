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
        History history = new History();
        string tool;
        List<Button> buttons = new List<Button>();
        StorageFolder savefolder;
        Brush buttoncolor;
        Windows.UI.Input.PointerPoint dragStart = null;
        int anchor = -1;
        ActionManager actionmanager = new ActionManager();

        public MainPage()
        {
            //Initializing program and fills a few necessary variables
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
            //If the move tool is selected, this part (initializes and) moves the selected shapes or group
            if (tool == "Move" && ((FrameworkElement)sender).Name == paintSurface.Name)
            {
                dragStart = e.GetCurrentPoint(paintSurface);
                Update(true);
            }
            //If the select tool is selected, this part will select or unselect any shape or group
            else if(tool == "Select" && ((FrameworkElement)sender).Name != paintSurface.Name)
            {
                lptag = Convert.ToInt32((e.OriginalSource as FrameworkElement).Tag);
                bool lptagselected = false;
                int actualselected = lptag;
                foreach(int c in selected)
                {
                    if (selected.Contains(lptag) || group.FindID(c).FindID(lptag).type != "Placeholder")
                    {
                        lptagselected = true;
                        actualselected = c;
                    }
                }
                if (!lptagselected)
                    selected.Add(lptag);
                else
                {
                    if (selected.Contains(lptag))
                        selected.Remove(lptag);
                    else
                        selected.Remove(actualselected);
                }
                if (selected.Count == 1)
                    anchor = selected[0];
                Update(false);
            }
            //This part initiates the drawing of a shape
            else if(tool == "Rectangle" || tool == "Ellipse")
            {
                itemcount++;
                dragStart = e.GetCurrentPoint(paintSurface);
                Composite newshape = new Composite(itemcount, tool);
                newshape.height = 1;
                newshape.width = 1;
                newshape.x = e.GetCurrentPoint(paintSurface).Position.X;
                newshape.y = e.GetCurrentPoint(paintSurface).Position.Y;
                Add addAction = new Add(group, newshape);
                actionmanager.takeAction(addAction);
                actionmanager.executeActions();
                Update(true);
                Update(false);
            }
        }

        //If the mouse button is released and a move action was being executed, then this cleans up after itself
        private void mouseUp(object sender, PointerRoutedEventArgs e) {
            if (tool == "Move")
            {
                dragStart = null;
            }
        }

        private void mouseMove(object sender, PointerRoutedEventArgs e)
        {
            //If the move tool is selected, this makes it possible to drag a selection
            if (tool == "Move" && dragStart != null && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                timeindex--; ;
                Update(false);
                var p2 = e.GetCurrentPoint(paintSurface);
                foreach(int tag in selected)
                {
                    Composite tmp = group.FindID(tag).Copy();
                    Move moveAction = new Move(tmp, p2.Position.X - dragStart.Position.X, p2.Position.Y - dragStart.Position.Y);
                    Replace replaceAction = new Replace(group, tmp, tag);
                    actionmanager.takeAction(moveAction);
                    actionmanager.takeAction(replaceAction);
                }
                actionmanager.executeActions();
                Update(true);
                Update(false);
            }
            //If the rectangle or ellipse tool is selected, then this creates the shape
            else if((tool == "Rectangle" || tool == "Ellipse") && dragStart != null && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                Undo_Click(sender, e);
                itemcount++;
                Composite newshape = new Composite(itemcount, tool);
                newshape.height = Math.Abs(dragStart.Position.Y - e.GetCurrentPoint(paintSurface).Position.Y);
                newshape.width = Math.Abs(dragStart.Position.X - e.GetCurrentPoint(paintSurface).Position.X);
                newshape.x = ReturnSmallest(dragStart.Position.X, e.GetCurrentPoint(paintSurface).Position.X);
                newshape.y = ReturnSmallest(dragStart.Position.Y, e.GetCurrentPoint(paintSurface).Position.Y);
                Add addAction = new Add(group, newshape);
                actionmanager.takeAction(addAction);
                actionmanager.executeActions();
                Update(true);
                Update(false);
            }
        }

        //If a single shape is selected, then scrolling the mouse will select groups higher up and then down again if scrolled the other way
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
        }

        //This button selects the move tool
        private void Move_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Move);
        }

        //This button resizes the selected shapes or group according to what is entered in the text boxes
        private void Resize_Click(object sender, RoutedEventArgs e)
        {
            if (RHeight.Text != "" && RWidth.Text != "")
            {
                foreach(int c in selected)
                {
                    Composite newcomposite = group.FindID(c).Copy();
                    Resize resizeAction = new Resize(newcomposite, Convert.ToDouble(RHeight.Text), Convert.ToDouble(RWidth.Text));
                    Replace replaceAction = new Replace(group, newcomposite, c);
                    actionmanager.takeAction(resizeAction);
                    actionmanager.takeAction(replaceAction);
                    actionmanager.executeActions();
                }
                Update(true);
                Update(false);
            }
        }

        //When havind groups and/or shapes selected, this button adds them to a group
        private void Group_Click(object sender, RoutedEventArgs e)
        {
            if (selected.Count != 0)
            {
                itemcount++;
                Add addActiona = new Add(group, new Composite(itemcount, "Group"));
                actionmanager.takeAction(addActiona);
                actionmanager.executeActions();
                foreach (int c in selected)
                {
                    Composite tmp = group.FindID(c).Copy();
                    Remove removeAction = new Remove(group, c);
                    Add addActionb = new Add(group.groupitems[group.groupitems.Count - 1], tmp);
                    actionmanager.takeAction(removeAction);
                    actionmanager.takeAction(addActionb);
                }
                actionmanager.executeActions();
                selected.Clear();
                anchor = -1;
                Update(true);
                Update(false);
            }
        }

        //This goes one step back
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

        //This goes one step forward
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

        //This returns the smalles double from the 2 inputs
        public static double ReturnSmallest(double first, double last)
        {
            if(first < last)
                return first;
            else
                return last;
        }


        //This changes the tool to an Ellipse
        private void Ellipse_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Ellipse);
        }

        //This changes the tool to a Rectangle
        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Rectangle);
        }

        //Update after undo/redo or changes made
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
            //false means that the history needs to be read and written to the paintsurface
            else
            {
                group = history.timeline[timeindex].composite.Copy();
                itemcount = history.timeline[timeindex].itemcount;
                paintSurface.Children.Clear();
                List<Composite> tosurface = history.timeline[timeindex].composite.Concatenate();
                foreach (Composite c in tosurface)
                {
                    Context context = new Context(DrawRectangle.getInstance());
                    if(c.type == "Ellipse")
                        context = new Context(DrawEllipse.getInstance());
                    context.x = c.x;
                    context.y = c.y;
                    context.height = c.height;
                    context.width = c.width;
                    context.id = c.id;
                    Shape shape = context.Draw();
                    SolidColorBrush brush = new SolidColorBrush();
                    if (IsSelected(c))
                        brush.Color = Colors.DarkSlateGray;
                    else
                        brush.Color = Colors.SlateGray;
                    shape.Fill = brush;
                    shape.PointerPressed += mouseDown;
                    shape.PointerMoved += mouseMove;
                    shape.PointerReleased += mouseUp;
                    shape.PointerWheelChanged += mouseScroll;
                    paintSurface.Children.Add(shape);
                }
            }
        }

        //Checks if composite object is selected
        public bool IsSelected(Composite c)
        {
            bool isselected = false;
            Composite tmp = c;
            if (selected.Contains(c.id))
                isselected = true;
            else
            {
                while (tmp.id != 0 && selected.Count > 0)
                {
                    int parent = group.Findparent(tmp.id);
                    if (selected.Contains(parent))
                    {
                        isselected = true;
                        break;
                    }
                    else
                        tmp = group.FindID(parent);
                }
            }
            return isselected;
        }
        
        //Selects the select tool
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Selectbutton(Select);
        }

        //Adds the ornament to the group or shape
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
            Save saveAction = new Save(group, writer, 0);
            actionmanager.takeAction(saveAction);
            actionmanager.executeActions();
            await writer.StoreAsync();
        }

        //Loads the shapes and groups from a file
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
            Load loadAction = new Load(group, lines);
            actionmanager.takeAction(loadAction);
            actionmanager.executeActions();
            Update(true);
            Update(false);
        }

        //Selects a specific tool
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