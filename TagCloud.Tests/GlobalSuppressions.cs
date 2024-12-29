// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Так делать явно плохо
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.Tests.ImageSaversTests.ImageSaverTest.SaveFile_SavesFile(System.String,System.String)~System.Boolean")]
[assembly: SuppressMessage("Interoperability", "CA1416:Проверка совместимости платформы", Justification = "<Ожидание>", Scope = "member", Target = "~M:TagCloud.Tests.ImageSaversTests.ImageSaverTest.SaveFile_ThrowsArgumentException_WithInvalidFilename(System.String)")]
