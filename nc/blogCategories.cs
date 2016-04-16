using System.Collections.Generic;

namespace nc
{
    public class blogCategories
    {
        public blogCategories()
        {
        }

        public Stack<string> cs = new Stack<string>();

        public Stack<string> catStack()
        {
            cs.Push("Entertainment");
            cs.Push("Etiquette");
            cs.Push("Events");
            cs.Push("Fashion");
            cs.Push("General");
            cs.Push("Guids");
            cs.Push("Locations");
            cs.Push("Music");
            cs.Push("News");
            cs.Push("Reviews");
            cs.Push("Tips");
            cs.Push("Trends");
            cs.Push("Upcoming");
            return cs;
        }
    }
}