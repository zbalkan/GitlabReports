# SemgrepReports

Parses Semgrep JSON reports and generates a PDF report.

## USAGE
```
SemgrepReports.exe [-short | --long] [args]

Short   Long    Function
-----   ----    --------
h       help    Displays help text and exits.
i       input   Path of json file.
o       output  Path of pdf file.

```

## Examples
```bash
SemgrepReports.exe -i reports.json -o reports.pdf
SemgrepReports.exe -i reports.json # output file will be saved into working directory as reports.pdf
```