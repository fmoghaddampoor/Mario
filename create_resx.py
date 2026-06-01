import os

def write_resx(path, entries):
    os.makedirs(os.path.dirname(path), exist_ok=True)
    with open(path, 'w', encoding='utf-8') as f:
        f.write('<?xml version="1.0" encoding="utf-8"?>\n')
        f.write('<root>\n')
        f.write('  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">\n')
        f.write('    <xsd:element name="root">\n')
        f.write('      <xsd:complexType>\n')
        f.write('        <xsd:choice maxOccurs="unbounded">\n')
        f.write('          <xsd:element name="data">\n')
        f.write('            <xsd:complexType>\n')
        f.write('              <xsd:sequence>\n')
        f.write('                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />\n')
        f.write('              </xsd:sequence>\n')
        f.write('            </xsd:complexType>\n')
        f.write('          </xsd:element>\n')
        f.write('        </xsd:choice>\n')
        f.write('      </xsd:complexType>\n')
        f.write('    </xsd:element>\n')
        f.write('  </xsd:schema>\n')
        f.write('  <resheader name="resmimetype"><value>text/microsoft-resx</value></resheader>\n')
        f.write('  <resheader name="version"><value>2.0</value></resheader>\n')
        f.write('  <resheader name="reader"><value>System.Resources.ResXResourceReader</value></resheader>\n')
        f.write('  <resheader name="writer"><value>System.Resources.ResXResourceWriter</value></resheader>\n')
        for name, value in entries:
            value_escaped = value.replace('&', '&amp;').replace('<', '&lt;').replace('>', '&gt;').replace('"', '&quot;')
            f.write('  <data name="' + name + '" xml:space="preserve"><value>' + value_escaped + '</value></data>\n')
        f.write('</root>\n')
    print('Created: ' + path)

write_resx('src/MarioEngine.Core/Resources/Resources.resx', [
    ('Game_Initializing', 'Game initializing'),
    ('Game_Initialized', 'Game initialized'),
    ('Game_LoadContent_Started', 'Game.LoadContent started'),
    ('Game_Shutdown_Started', 'Game.Shutdown started'),
    ('Game_ShuttingDown', 'Game shutting down'),
    ('Logging_Initialized', 'Logging initialized. Seq: {Seq}, Loki: {Loki}'),
    ('DI_Container_Initialized', 'DI container initialized'),
])

write_resx('src/MarioEngine.Desktop/Resources/Resources.resx', [
    ('Window_Title', 'Super Mario \u2014 v'),
    ('Window_Starting', 'Window starting'),
    ('Window_Opened', 'Window opened: {Width}x{Height}, GL {Major}.{Minor}'),
    ('Framebuffer_Resized', 'Framebuffer resized: {Width}x{Height}'),
    ('Splash_Created', 'Splash screen created'),
    ('Splash_Finished', 'Splash finished, starting game'),
    ('GL_NotAvailable', 'OpenGL context not available until window is loaded'),
    ('Shader_LinkFailed', 'Shader program link failed: {Info}'),
    ('Shader_CompileFailed', 'Shader compile failed ({Type}): {Info}'),
    ('Splash_NotFound', 'Splash image not found: {Path}'),
    ('Fatal_UnhandledException', 'Unhandled exception \u2014 game crashed'),
    ('Fatal_ErrorMessage', 'Fatal error: {Message}'),
])

print('All resource files created')
