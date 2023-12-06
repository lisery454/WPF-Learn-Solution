using System.Diagnostics;
using ReadData;

const string mainFilePath = @"C:\Users\lxl11\Desktop\datademo_cpsc_2020_1\1(数据).txt";
const string newMainFilePath = @"C:\Users\lxl11\Desktop\datademo_cpsc_2020_1\data\main.txt";
const string rPeaksFilePath = @"C:\Users\lxl11\Desktop\datademo_cpsc_2020_1\1-Rpeaks(R波波峰).txt";
const string newRPeaksFilePath = @"C:\Users\lxl11\Desktop\datademo_cpsc_2020_1\data\r_peaks.txt";

// DataReformat.Reformat(filePath, newFilePath);

var dataReader = new MainDataReader(newMainFilePath, 400);

Console.WriteLine(dataReader.ValueAt(TimeSpan.FromSeconds(5)));
// var begin = DateTime.Now;
// for (var i = 0; i < 100; i++)
// {
//     var data = dataReader.GetData(TimeSpan.FromSeconds(i), TimeSpan.FromSeconds(10 ));
// }
//
// Console.WriteLine(dataReader.TotalTime);
// var end = DateTime.Now;
// Console.WriteLine((end - begin).TotalSeconds);

// DataReformat.ReformatRPeaks(rPeaksFilePath, newRPeaksFilePath);

// var rPeaksReader = new RPeaksReader(newRPeaksFilePath, 240);

// Console.WriteLine(rPeaksReader.GetAllRPeaksNum());
//
// Console.WriteLine(rPeaksReader.ValueAt(0));
// Console.WriteLine(rPeaksReader.ValueAt(1));
// Console.WriteLine(rPeaksReader.ValueAt(2));
// Console.WriteLine(rPeaksReader.ValueAt(rPeaksReader.GetAllRPeaksNum() - 1));

// var data = rPeaksReader.GetData(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10000));
// data.ForEach(span => Console.WriteLine(span.TotalMilliseconds));