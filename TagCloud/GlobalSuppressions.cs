// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;


// Так делать явно плохо
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.CloudLayouterPainters.CloudLayouterPainter.DrawText(System.Drawing.Graphics,System.Drawing.Rectangle,System.String)")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.CloudLayouterPainters.CloudLayouterPainter.Draw(System.Collections.Generic.IList{TagCloud.Tag})~System.Drawing.Bitmap")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.CloudLayouterPainters.CloudLayouterPainter.FindFittingFontSize(System.Drawing.Graphics,System.String,System.Drawing.Rectangle)~System.Int32")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:Program.ParseFont(System.String)~System.String")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.ImageSavers.ImageSaver.SaveFile(System.Drawing.Bitmap,System.String,System.String)")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.Parsers.FontParser.ParseFont(System.String)~System.String")]
