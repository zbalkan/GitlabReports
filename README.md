# GitlabReports (former SemgrepReports)
[![Semgrep](https://github.com/zbalkan/GitlabReports/actions/workflows/semgrep.yml/badge.svg?branch=master)](https://github.com/zbalkan/GitlabReports/actions/workflows/semgrep.yml)
[![.NET](https://github.com/zbalkan/GitlabReports/actions/workflows/dotnet.yml/badge.svg?branch=master)](https://github.com/zbalkan/GitlabReports/actions/workflows/dotnet.yml)

Parses Gitlab code quality and Semgrep JSON reports and generates a PDF report. The executable uses shorthand `sr` from _SemgrepReports_, the initial name.

## USAGE
```
sr [-short | --long] [args]

Short   Long    Function
-----   ----    --------
h       help    Displays help text and exits.
i       input   Path of JSON file.
o       output  Path of PDF file.

```

## Examples
On Windows:
```bash
sr.exe -i reports.json -o reports.pdf
sr.exe -i reports.json # output file will be saved into working directory as reports.pdf
```

On Linux:
```bash
./sr -i reports.json -o reports.pdf
./sr -i reports.json # output file will be saved into working directory as reports.pdf
```
