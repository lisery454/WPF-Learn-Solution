using System.Text;

namespace ReadData;

public class RPeaksReader 
{
    private string _path;
    private FileStream _fileStream;
    private int _frequency;
    private const int Width = 14;
    private byte[] buffer = new byte[Width];
    private StringBuilder _stringBuilder;

    public RPeaksReader(string filePath, int frequency)
    {
        _path = filePath;
        _frequency = frequency;
        _fileStream = new FileStream(filePath, FileMode.Open);
        _stringBuilder = new StringBuilder();
    }

    public int GetAllRPeaksNum()
    {
        var fileInfo = new FileInfo(_path);
        return (int)((float)fileInfo.Length / Width);
    }

    public long ValueAt(int index)
    {
        _fileStream.Seek(index * Width, SeekOrigin.Begin);
        var read = _fileStream.Read(buffer, 0, Width);
        if (read == Width)
        {
            _stringBuilder.Clear();
            foreach (var t in buffer) _stringBuilder.Append((char)t);
            if (long.TryParse(_stringBuilder.ToString(), out var res))
                return res;
        }

        throw new Exception($"read {_stringBuilder} wrong");
    }

    public int FindFirstIndexBiggerEqualThen(long value)
    {
        var begin = 0;
        var end = GetAllRPeaksNum();
        while (true)
        {
            if (begin >= end - 1)
            {
                return ValueAt(begin) >= value ? begin : end;
            }

            var middle = (end + begin) / 2;

            if (ValueAt(middle) < value)
            {
                begin = middle + 1;
            }
            else if (ValueAt(middle) > value)
            {
                end = middle;
            }
            else
            {
                return middle;
            }
        }
    }

    public List<TimeSpan> GetData(TimeSpan beginTime, TimeSpan lastTime)
    {
        var beginFrame = (int)(beginTime.TotalSeconds * _frequency);
        var endFrame = (int)(lastTime.TotalSeconds * _frequency) + beginFrame;

        var res = new List<TimeSpan>();

        var beginIndex = FindFirstIndexBiggerEqualThen(beginFrame);
        var endIndex = FindFirstIndexBiggerEqualThen(endFrame);

        for (var i = beginIndex; i < endIndex; i++)
        {
            res.Add(TimeSpan.FromSeconds(ValueAt(i) / (float)_frequency));
        }

        return res;
    }
}