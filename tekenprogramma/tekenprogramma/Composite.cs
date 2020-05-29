using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace tekenprogramma
{
    public class Composite
    {
        //Intializes variables
        public double height;
        public double width;
        public double x;
        public double y;
        public string type;
        public int id;
        public List<Composite> groupitems;
        public List<Ornament> ornaments;

        public Composite(int id, string type)
        {
            //Filles necessary variables on create
            this.type = type;
            this.id = id;
            height = 0;
            width = 0;
            x = 0;
            y = 0;
            groupitems = new List<Composite>();
            this.ornaments = new List<Ornament>();
        }

        //Adds a composite object onderneath this one
        public void Add(Composite newcomposite)
        {
            groupitems.Add(newcomposite);
        }

        //Accept method for visitor pattern
        public void Accept(Visitor visitor)
        {
            visitor.visit(this);
        }

        //Find a composite object by it's id and return it
        public Composite FindID(int id)
        {
            if (this.id == id)
                return this;
            foreach(Composite c in groupitems)
            {
                if(c.id == id)
                {
                    return c;
                }
                else
                {
                    Composite tmp = c.FindID(id);
                    if (tmp.id == id)
                    {
                        return tmp;
                    }
                }
            }
            return new Composite(746, "Placeholder");
        }

        public XYXY GetGroupXYHW()
        {
            XYXY xyhw = new XYXY();
            foreach(Composite c in groupitems)
            {
                if(c.type != "Group")
                {
                    if (xyhw.sx != 746746)
                        xyhw.sx = MainPage.ReturnSmallest(xyhw.sx, c.x);
                    else
                        xyhw.sx = c.x;
                    if (xyhw.sy != 746746)
                        xyhw.sy = MainPage.ReturnSmallest(xyhw.sy, c.y);
                    else
                        xyhw.sy = c.y;
                    if (xyhw.ex != 746746)
                        xyhw.ex = MainPage.ReturnLargest(xyhw.ex, c.x + c.width);
                    else
                        xyhw.ex = c.x + c.width;
                    if (xyhw.ey != 746746)
                        xyhw.ey = MainPage.ReturnLargest(xyhw.ey, c.y + c.height);
                    else
                        xyhw.ey = c.y + c.height;
                }
                else
                {
                    XYXY rxyhw = c.GetGroupXYHW();
                    if (xyhw.sx != 746746)
                        xyhw.sx = MainPage.ReturnSmallest(xyhw.sx, c.x);
                    else
                        xyhw.sx = c.x;
                    if (xyhw.sy != 746746)
                        xyhw.sy = MainPage.ReturnSmallest(xyhw.sy, c.y);
                    else
                        xyhw.sy = c.y;
                    if (xyhw.ex != 746746)
                        xyhw.ex = MainPage.ReturnLargest(xyhw.ex, c.x + c.width);
                    else
                        xyhw.ex = c.x + c.width;
                    if (xyhw.ey != 746746)
                        xyhw.ey = MainPage.ReturnLargest(xyhw.ey, c.y + c.height);
                    else
                        xyhw.ey = c.y + c.height;
                }
            }
            return xyhw;
        }

        //Replace a composite object by it's id
        public void SetID(Composite replacement, int id)
        {
            if (this.id == id)
            {
                height = replacement.height;
                width = replacement.width;
                groupitems = replacement.groupitems;
                id = replacement.id;
                type = replacement.type;
                x = replacement.x;
                y = replacement.y;
                return;
            }
            for(int c = 0; c < groupitems.Count; c++)
            {
                groupitems[c].SetID(replacement, id);
            }
        }

        //Remove by id
        public void RemoveID(int id)
        {
            for(int c = 0; c < groupitems.Count; c++)
            {
                if (groupitems[c].id == id)
                {
                    groupitems.Remove(groupitems[c]);
                }
                else
                {
                    groupitems[c].RemoveID(id);
                }
            }
        }

        //Make 1 long list of all groups and shapes to draw on the paint surface
        public List<Composite> Concatenate()
        {
            List<Composite> list = new List<Composite>();
            foreach(Composite c in groupitems)
            {
                list.Add(c);
                if (c.groupitems.Count != 0)
                {
                    list.AddRange(c.Concatenate());
                }
            }
            return list;
        }

        //Make a copy of this object, usefull for making the history, since it is a reference type and can't be used otherwise
        public Composite Copy()
        {
            Composite tmp = new Composite(this.id, this.type);
            tmp.height = this.height;
            tmp.width = this.width;
            tmp.x = this.x;
            tmp.y = this.y;
            foreach(Composite c in groupitems)
            {
                tmp.Add(c.Copy());
            }
            foreach (Ornament c in ornaments)
                tmp.ornaments.Add(c.Copy());
            return tmp;
        }

        //Returns the id of the parent of a composite by id
        public int Findparent(int tag)
        {
            int temptag = -1;
            foreach(Composite c in groupitems)
            {
                if (c.id == tag)
                    return id;
                else
                    temptag = c.Findparent(tag);
                if (temptag != -1)
                    return temptag;
            }
            return -1;
        }

        //Save to file recursively
        public void Savetofile(DataWriter write, int depth)
        {
            foreach(Ornament o in ornaments)
            {
                for (int c = 0; c < depth; c++)
                {
                    write.WriteString("\t");
                }
                write.WriteString("Ornament " + o.position.ToLower() + " \"" + o.ornament + "\"\n");
            }
            for (int c = 0; c < depth; c++)
            {
                write.WriteString("\t");
            }
            if (type == "Group")
            {
                write.WriteString(type + " " + groupitems.Count + "\n");
                foreach (Composite c in groupitems)
                {
                    c.Savetofile(write, depth + 1);
                }
            }
            else
            {
                write.WriteString(type + " " + x + " " + y + " " + width + " " + height + "\n");
            }
        }

        //Load from file recursively
        public void Loadfromfile(List<string> lines)
        {
            List<Ornament> newornaments = new List<Ornament>();
            while(lines.Count > 0)
            {
                string currentline = "";
                int tabcount = 0;
                foreach(char c in lines[0])
                {
                    if (c == '\t')
                        tabcount++;
                    else
                        currentline = currentline + c;
                }
                List<string> split = currentline.Split(' ').ToList();
                if(split[0] != "Ornament")
                {
                    MainPage.itemcount++;
                    Add(new Composite(MainPage.itemcount, split[0]));
                }
                int nexttabcount = 0;
                if (lines.Count > 1)
                {
                    for (int c = 0; lines[1][c] == '\t'; c++)
                    {
                        nexttabcount++;
                    }
                }
                else
                    nexttabcount--;
                lines.RemoveAt(0);
                if(split[0] == "Ornament")
                {
                    split[2] = split[2].Replace("\"", "");
                    newornaments.Add(new Ornament(split[2], split[1]));
                }
                if(split[0] != "Group" && split[0] != "Ornament")
                {
                    groupitems[groupitems.Count - 1].x = Convert.ToDouble(split[1]);
                    groupitems[groupitems.Count - 1].y = Convert.ToDouble(split[2]);
                    groupitems[groupitems.Count - 1].width = Convert.ToDouble(split[3]);
                    groupitems[groupitems.Count - 1].height = Convert.ToDouble(split[4]);
                    groupitems[groupitems.Count - 1].ornaments = newornaments;
                }
                if (nexttabcount > tabcount)
                {
                    groupitems[groupitems.Count - 1].Loadfromfile(lines);
                }
                else if (nexttabcount < tabcount)
                {
                    return;
                }
            }
        }

        //Recursively changes size, works for shapes, but is mainly handy for groups
        public void RChangeSize(double newheight, double newwidth)
        {
            if(type != "Group")
            {
                height = newheight;
                width = newwidth;
            }
            else
            {
                foreach(Composite c in groupitems)
                {
                    c.RChangeSize(newheight, newwidth);
                }
            }
        }

        //Recursively changes position, works for shapes, but is mainly handy for groups
        public void RMove(double movex, double movey)
        {
            if (type != "Group")
            {
                x = x + movex;
                y = y + movey;
            }
            else
            {
                foreach (Composite c in groupitems)
                {
                    c.RMove(movex, movey);
                }
            }
        }
    }
}
