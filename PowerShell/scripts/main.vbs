Set WSHShell = CreateObject("Wscript.Shell")
sCurrentDirectory = WSHShell.CurrentDirectory & "\"

Set oShell = CreateObject("Shell.Application")
oShell.ShellExecute "powershell", "-executionpolicy bypass -NonInteractive -windowstyle Hidden -file " & sCurrentDirectory & "main.ps1", "", "runas", 0