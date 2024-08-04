do {
    $input = Read-Host "输入字符串 [回车则退出]"
    Write-Output $input.ToUpper()
} while ($input -ne '')
