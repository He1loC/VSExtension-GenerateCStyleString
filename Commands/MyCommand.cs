using System.Linq;

namespace GenerateCStyleString
{
    [Command(PackageIds.MyCommand)]
    internal sealed class MyCommand : BaseCommand<MyCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var docView = await VS.Documents.GetActiveDocumentViewAsync();
            var selection = docView?.TextView.Selection.SelectedSpans.FirstOrDefault();
            if (selection.HasValue) 
            {
                var text = selection.Value.GetText();
                var cstring = "char sz" + text + "[] = {";
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
