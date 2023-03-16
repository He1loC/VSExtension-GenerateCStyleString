using System.Linq;

namespace GenerateCStyleString.Commands
{
    [Command(PackageIds.GenUsingAndStrCMD)]
    internal sealed class GenUsingAndStrCMD : BaseCommand<GenUsingAndStrCMD>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            var selection = docView?.TextView.Selection.SelectedSpans.FirstOrDefault();
            if (selection.HasValue)
            {
                var text = selection.Value.GetText();
                var cstring = "using PFN_" + text + " = decltype("+text+");\n";
                cstring += "char sz" + text + "[] = {";
                foreach (var ch in text)
                {
                    cstring += $"'{ch}',";
                }
                cstring += "'\\0'};";
                docView.TextBuffer.Replace(selection.Value, cstring);
            }
        }
    }
}
