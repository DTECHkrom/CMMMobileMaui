using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.Models.GUI
{
    internal class FilterDisplayName
    {
        #region FIELDS

        private string name;
        private string changedName;

        #endregion

        #region Cstr

        public FilterDisplayName(string name)
        {
            this.name = name;
        }

        #endregion

        #region METHOD GetDisplayNameFrom

        public string GetDisplayName() =>
            string.IsNullOrEmpty(changedName) ? ChangeNameToDisplayName()
            : changedName;

        #endregion

        #region METHOD ChangeNameToDisplayName
        private string ChangeNameToDisplayName()
        {
            //StringBuilder sb = new StringBuilder();

            //sb.Append(TakeFirstLetter());
            //int index = 0;

            //SkipFirtLetter().ForEach(letter =>
            //{
            //    CheckLetterForUppercase(letter, sb, index);

            //    ++index;
            //});

            changedName = Container.TextDictionaryResource.GetText(name);

            return changedName;
        }

        #endregion

        #region METHOD CheckLetterForUppercase

        private void CheckLetterForUppercase(char letter, StringBuilder sb, int index)
        {
            if (IsCurrentAndBeforeUpper(letter, index))
            {
                sb.Append(letter);
            }
            else
            {
                IfLetterIsUpperAddSpaceBefore(letter, sb);

                if (IsNextLetterUpper(index))
                {
                    sb.Append(letter);
                }
                else
                {
                    sb.Append(char.ToLowerInvariant(letter));
                }
            }
        }

        #endregion

        #region METHOD IsNextLetterUpper

        private bool IsNextLetterUpper(int index) =>
            index + 2 < name.Length
            && char.IsUpper(name[index + 2]);

        #endregion

        #region METHOD IsCurrentAndBeforeUpper

        private bool IsCurrentAndBeforeUpper(char letter, int index) =>
            (char.IsUpper(name[index]))
                            && char.IsUpper(letter);

        #endregion

        #region SkipFirtLetter

        private IEnumerable<char> SkipFirtLetter() =>
            name.Skip(1);

        #endregion

        #region METHOD TakeFirstLetter

        private char TakeFirstLetter() =>
            name[0];

        #endregion

        #region METHOD IfLetterIsUpperAddSpaceBefore

        private void IfLetterIsUpperAddSpaceBefore(char letter, StringBuilder sb)
        {
            if (char.IsUpper(letter))
            {
                sb.Append(" ");
            }
        }

        #endregion
    }
}
