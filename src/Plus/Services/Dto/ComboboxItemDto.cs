using System;

namespace Plus.Services.Dto
{
    [Serializable]
    public class ComboboxItemDto : IDto
    {
        public string Value { get; set; }

        public string DisplayText { get; set; }

        public bool IsSelected { get; set; }

        public ComboboxItemDto()
        {
        }

        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}