import entries

XmlDeclaration = '<?xml version="1.0" encoding="utf-8"?>'

def SetContentTypeToXml(handler):
    handler.response.headers['Content-Type'] = 'text/xml'

def getHtmlFrame():
    return '''<html><head>
                   <style type="text/css">
                       body{
                            font-family: arial;
                            padding:0;
                            margin:0;
                       }
                       td{
                           border:solid 1px #d4d4d4;
                           font-size: 9pt;
                       }
                       tr.headrow{
                           color: #008800;
                           font-weight: bold;
                       }
                       tr.headRow td{
                           border:0;
                        }
                       .header{
                           color: maroon;
                           font-weight: bold;
                           font-size: 11pt;
                        }
                       .link{
                           color: Blue;
                           cursor: pointer;
                       }
                       .link:hover{
                           text-decoration: underline;
                       }
                       </style></head><body>'''

def GetEntriesHTML(fileName, filexml):
    html = getHtmlFrame()
    html += '<span class="header">List of Entries in File: ' + fileName + '</span>'
    html += '<table cellspacing="0" cellpadding="5"><tr><td>&nbsp;</td><td>Date</td><td>Day/Night</td><td>Notes</td></tr>'
    lists = entries.EntryLists(filexml)
    count = 0
    for entry in [e for e in lists.entryList if not e.isInvisible]:
       count += 1
       html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
       html += '<td width="20">%s</td><td>%s</td><td>%s</td><td width="460">%s</td></tr>' % (count, entry.date, entry.dn, entry.notes or '&nbsp;')
    html += '</table>'
    kavuahs = lists.kavuahList
    if kavuahs and kavuahs.count > 0:
        count = 0
        html += '''<br /><span class="header">List of Kavuahs in File: %s</span>
            <table cellspacing="0" cellpadding="5"><tr><td>&nbsp;</td><td>Active?</td><td>Type</td>
            <td>Number</td><td>Day/Night</td><td>Cancels?</td><td>Notes</td></tr>''' % (fileName)
        for kavuah in kavuahs:
            count += 1
            html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
            html += '''<td width="20">%s</td><td>%s</td><td>%s</td><td>%s</td><td>%s</td>
                <td>%s</td><td width="460">%s</td>
                </tr>''' % (count, 'Yes' if kavuah.active else 'No', kavuah.type, kavuah.number, kavuah.dn, 'Yes' if kavuah.cancels else 'No', kavuah.notes or '&nbsp;')
        html += '</table>'
    html += '</body></html>'
    return html;