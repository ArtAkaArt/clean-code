﻿using Markdown.ITagsInterfaces;

namespace Markdown
{
    public class Marker
    {
        private readonly ITagsFinder finder;
        private readonly ITagsFilter filter;
        private readonly ITagsSwitcher switcher;

        public Marker(ITagsFinder finder, ITagsFilter filter, ITagsSwitcher switcher)
        {
            this.finder = finder;
            this.filter = filter;
            this.switcher = switcher;
        }

        public string Mark(string text)
        {
            var paragraphs = text.Split(
                new string[] { "\r\n" },
                StringSplitOptions.None
            );
            var result = new string[paragraphs.Length];
            for (int i = 0; i < paragraphs.Length; i++)
            {
                var tags = finder.CreateTagList(paragraphs[i]);
                tags = filter.FilterTags(tags, paragraphs[i]);
                result[i] = switcher.SwitchTags(tags, paragraphs[i]);
            }

            return string.Join("\r\n", result);
        }
    }
}