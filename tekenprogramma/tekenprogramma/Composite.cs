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
        }

        public void Add(Composite newcomposite)
        {
            groupitems.Add(newcomposite);
        }

        //Find a group or shape by it's id
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

        //Set Replace a group or shape by it's id
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
                if(c.groupitems.Count == 0)
                {
                    list.Add(c);
                }
                else
                {
                    list.AddRange(c.Concatenate());
                }
            }
            return list;
        }

        //Make a copy of this object, because it's reference type
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
            return tmp;
        }

        //Count the shapes for save purposes
        public int Countfigures()
        {
            int c = 0;
            foreach(Composite a in groupitems)
            {
                c++;
            }
            return c;
        }

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
            for (int c = 0; c < depth; c++)
            {
                write.WriteString("\t");
            }

            if (type == "Group")
            {
                write.WriteString(type + " " + Countfigures() + "\n");
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
                MainPage.itemcount++;
                Add(new Composite(MainPage.itemcount, split[0]));
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
                if(split[0] != "Group")
                {
                    groupitems[groupitems.Count - 1].x = Convert.ToDouble(split[1]);
                    groupitems[groupitems.Count - 1].y = Convert.ToDouble(split[2]);
                    groupitems[groupitems.Count - 1].width = Convert.ToDouble(split[3]);
                    groupitems[groupitems.Count - 1].height = Convert.ToDouble(split[4]);
                }
                if (nexttabcount > tabcount)
                {
                    groupitems[groupitems.Count - 1].Loadfromfile(lines);
                }
                else if (nexttabcount < tabcount)
                {
                    return;
                }
                /*if (lines.Count > 0)
                {
                    lines.RemoveAt(0);
                }*/
            }
        }

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
