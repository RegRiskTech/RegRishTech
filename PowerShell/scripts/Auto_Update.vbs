Set WSHShell = CreateObject("Wscript.Shell")
sCurrentDirectory = WSHShell.CurrentDirectory & "\"

Set oShell = CreateObject("Shell.Application")  
oShell.ShellExecute "powershell", "-executionpolicy bypass -file " & sCurrentDirectory & "Auto_Update.ps1", "", "runas", 0