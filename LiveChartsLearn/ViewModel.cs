using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using Microsoft.Win32;
using ReadData;
using SkiaSharp;

namespace LiveChartsLearn;

public partial class ViewModel : ObservableObject
{
    private MainDataReader? _dataReader;
    private RPeaksReader? _rPeaksReader;

    private float _timeInterval = 10;

    [ObservableProperty] private double _currentProcess;

    public List<Axis> XAxes { get; set; } = new()
    {
        new TimeSpanAxis(TimeSpan.FromMilliseconds(1), date => date.ToString())
        {
            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
                { StrokeThickness = 1, PathEffect = new DashEffect(new float[] { 3, 3 }) },
            MinStep = 1
        },
    };

    public List<Axis> YAxes { get; set; } = new()
    {
        new Axis
        {
            LabelsPaint = new SolidColorPaint(SKColors.DarkSlateBlue),
            TextSize = 10,

            SeparatorsPaint = new SolidColorPaint(SKColors.LightSlateGray)
            {
                StrokeThickness = 1,
                PathEffect = new DashEffect(new float[] { 3, 3 })
            },
            CustomSeparators = new[]
            {
                // -2, -1.8, -1.6, -1.4, -1.2, -1, -0.8, -0.6, -0.4, -0.2, 0, 0.2, 0.4, 0.6, 0.8, 1, 1.2, 1.4, 1.6, 1.8, 2
                -2, -1.5, -1, -0.5, 0, 0.5, 1, 1.5, 2
            }
        }
    };

    public ISeries[] Series { get; set; } =
    {
        new LineSeries<TimeSpanPoint>
        {
            GeometryStroke = null,
            GeometryFill = null,
            Values = new ObservableCollection<TimeSpanPoint>(),
            Fill = null,
            LineSmoothness = 1,
            Stroke = new SolidColorPaint(SKColors.MediumPurple, 2)
        },
        new ScatterSeries<TimeSpanPoint>
        {
            Values = new ObservableCollection<TimeSpanPoint>(),
            Fill = new SolidColorPaint(new SKColor(147, 112, 219, 100)),
            Stroke = new SolidColorPaint(SKColors.DarkSlateBlue, 3),
            GeometrySize = 10,
            MinGeometrySize = 5,
        }
    };

    [ObservableProperty] private string _mainFileName = "not open file";
    [ObservableProperty] private string _rPeaksFileName = "not open file";


    public ViewModel()
    {
        _dataReader = null;
        _rPeaksReader = null;
    }

    partial void OnCurrentProcessChanged(double value)
    {
        UpdateMain(value);
        UpdateRPeaks(value);
    }

    [RelayCommand]
    private void OpenMainFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() is true)
        {
            MainFileName = openFileDialog.FileName;
            _dataReader = new MainDataReader(MainFileName, 400);
            UpdateMain(CurrentProcess);
            UpdateRPeaks(CurrentProcess);
        }
    }

    [RelayCommand]
    private void OpenRPeakFile()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() is true)
        {
            RPeaksFileName = openFileDialog.FileName;
            _rPeaksReader = new RPeaksReader(RPeaksFileName, 240);
            UpdateRPeaks(CurrentProcess);
        }
    }

    private void UpdateRPeaks(double process)
    {
        if (_rPeaksReader != null && _dataReader != null)
        {
            var beginSeconds = _dataReader.TotalTime.TotalSeconds * process;
            var data = _rPeaksReader.GetData(TimeSpan.FromSeconds(beginSeconds), TimeSpan.FromSeconds(_timeInterval));
            var observableCollection = (ObservableCollection<TimeSpanPoint>)Series[1].Values!;
            observableCollection.Clear();
            foreach (var t in data)
            {
                observableCollection.Add(
                    new TimeSpanPoint(t, _dataReader.ValueAt(t)));
            }
        }
    }


    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    private void UpdateMain(double process)
    {
        if (_dataReader != null)
        {
            var beginSeconds = _dataReader.TotalTime.TotalSeconds * process;
            var data = _dataReader.GetData(TimeSpan.FromSeconds(beginSeconds), TimeSpan.FromSeconds(_timeInterval));
            var observableCollection = (ObservableCollection<TimeSpanPoint>)Series[0].Values!;
            observableCollection.Clear();
            for (var i = 0; i < data.Count; i++)
            {
                observableCollection.Add(
                    new TimeSpanPoint(
                        TimeSpan.FromMilliseconds(beginSeconds * 1000f + _timeInterval * i * 1000f / data.Count),
                        data[i]));
            }
        }
    }
}