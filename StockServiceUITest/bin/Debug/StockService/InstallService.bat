%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe DataCollector.exe
Net Start StockService
sc config StockService start= auto
pause