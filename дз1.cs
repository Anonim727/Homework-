Console.WriteLine("Type\tByte(s) of memory\tMin\tMax");

Console.WriteLine(
   format: "{0}\t{1}\t{2}\t{3}",
   "sbyte",
   $"{sizeof(sbyte)}",
   $"{sbyte.MinValue:N0}",
   $"{sbyte.MaxValue:N0}");

Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "short",
    $"{sizeof(short)}",
    $"{short.MinValue:N0}",
    $"{short.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "ushort",
    $"{sizeof(ushort)}",
    $"{ushort.MinValue:N0}",
    $"{ushort.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "int",
    $"{sizeof(int)}",
    $"{int.MinValue:N0}",
    $"{int.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "uint",
    $"{sizeof(uint)}",
    $"{uint.MinValue:N0}",
    $"{uint.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "long",
    $"{sizeof(long)}",
    $"{long.MinValue:N0}",
    $"{long.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "ulong",
    $"{sizeof(ulong)}",
    $"{ulong.MinValue:N0}",
    $"{ulong.MaxValue:N0}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "float",
    $"{sizeof(float)}",
    $"{float.MinValue:E}",
    $"{float.MaxValue:E}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "double",
    $"{sizeof(double)}",
    $"{double.MinValue:E}",
    $"{double.MaxValue:E}");
Console.WriteLine(
    format: "{0}\t{1}\t{2}\t{3}",
    "decimal",
    $"{sizeof(decimal)}",
    $"{decimal.MinValue:N0}",
    $"{decimal.MaxValue:N0}");`