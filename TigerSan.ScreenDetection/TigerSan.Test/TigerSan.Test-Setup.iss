 ; 由Inno Setup Script向导生成的脚本。

#define MyAppName "12345678"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "bilibli 真的虎子君"
#define MyAppURL "https://space.bilibili.com/34323512"

[Setup]
;AppId：应用程序的唯一标识。
AppId={{12345678-2025-bili-bili-RealTigerSan}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
;PrivilegesRequired：取消注释以在非管理安装模式下运行以下行（仅为当前用户安装）。
;PrivilegesRequired=lowest
OutputDir="0 Output"
SetupIconFile={#MyAppName}\Logos\logo.ico
OutputBaseFilename={#MyAppName}-{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "{#MyAppName}\bin\Debug\net8.0-windows\*"; \
    DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; \
    Excludes: "Settings\*,Log\*,Character\*"
;注意：不要在任何共享的系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

