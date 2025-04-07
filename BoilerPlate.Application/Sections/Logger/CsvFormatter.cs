using Serilog.Events;
using Serilog.Formatting;
using System.IO;

namespace BoilerPlate.Application.Sections.Logger
{
    public class CsvFormatter : ITextFormatter
    {
        private bool _needsHeaders = true;

        public void Format(LogEvent logEvent, TextWriter textWriter)
        {
            if (_needsHeaders)
            {
                WriteHeaders(textWriter);
                _needsHeaders = false;
            }

            textWriter.Write($"{logEvent.Timestamp.ToString("o")},");
            textWriter.Write($"{logEvent.Level},");
            textWriter.Write($"\"{logEvent.MessageTemplate.Render(logEvent.Properties).Replace("\"", "\"\"")}\",");
            textWriter.Write($"{string.Join("|", logEvent.Properties.Select(p => $"{p.Key}=\"{p.Value.ToString().Replace("\"", "\"\"")}\""))}");
            textWriter.WriteLine();
        }

        private static void WriteHeaders(TextWriter textWriter)
        {
            textWriter.WriteLine("Timestamp,Level,Message,Properties");
        }

        public void ResetNeedsHeaders()
        {
            _needsHeaders = true;
        }
    }
}
