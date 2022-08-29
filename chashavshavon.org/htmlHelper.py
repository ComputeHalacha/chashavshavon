import entries


def SetContentTypeToJson(handler):
    handler.response.headers['Content-Type'] = 'text/json'


def getHtmlFrame():
    return '''<html><head>
                   <style type="text/css">
                       body{
                            font-family: arial;
                            padding:0;
                            margin:0;
                            direction:rtl;
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


def GetEntriesHTML(filename, json_text):
    html = getHtmlFrame()
    html += '<span class="header">' \
            '&#1512;&#1513;&#1497;&#1502;&#1514; &#1512;&#1488;&#1497;&#1497;&#1514; ' \
            '&#1489;&#1511;&#1493;&#1489;&#1509;: ' + \
            filename + '</span>'
    html += '<table cellspacing="0" cellpadding="5"><tr><td>#</td><td>&#1514;&#1488;&#1512;&#1497;&#1498;</td>' \
            '<td>&#1497;&#1493;&#1501;\\&#1500;&#1497;&#1500;&#1492;</td>' \
            '<td>&#1492;&#1506;&#1512;&#1493;&#1514;</td></tr>'
    lists = entries.EntryLists(json_text)
    count = 0
    for entry in [e for e in lists.entryList if not e.isInvisible]:
        count += 1
        html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
        html += '<td width="20">%s</td><td>%s</td><td>%s</td><td width="460">%s</td></tr>' % (
            count,
            entry.date,
            "&#1497;&#1493;&#1501;" if entry.dn == 'Day' else "&#1500;&#1497;&#1500;&#1492;",
            entry.notes or '&nbsp;')
    html += '</table>'
    kavuahs = lists.kavuahList
    if kavuahs and kavuahs.count > 0:
        count = 0
        html += '''<br /><span class="header">&#1512;&#1513;&#1497;&#1502;&#1514;
            &#1511;&#1489;&#1493;&#1506;: %s</span>
            <table cellspacing="0" cellpadding="5"><tr><td>#</td><td>&#1508;&#1506;&#1497;&#1500;?</td>
            <td>&#1505;&#1493;&#1490;</td>
            <td>&#1506;&#1493;&#1504;&#1492;</td>
            <td>&#1502;&#1489;&#1496;&#1500; &#1506;"&#1489;?</td><td>&#1492;&#1506;&#1512;&#1493;&#1514;</td>
            </tr>''' % filename
        for kavuah in kavuahs:
            count += 1
            html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
            html += '''<td width="20">%s</td><td>%s</td><td>%s</td><td>%s</td><td>%s</td><td width="460">%s</td>
                </tr>''' % (count,
                            '&#1499;&#1503;' if kavuah.active else '&#1500;&#1488;',
                            kavuah.type,
                            "&#1497;&#1493;&#1501;" if kavuah.dn == 'Day' else "&#1500;&#1497;&#1500;&#1492;",
                            '&#1499;&#1503;' if kavuah.cancels else '&#1500;&#1488;',
                            kavuah.notes or '&nbsp;')
        html += '</table>'
        taharaEvents = lists.taharaEventsList
        if taharaEvents and taharaEvents.count > 0:
            count = 0
            html += '''<br /><span class="header">&#1512;&#1513;&#1497;&#1502;&#1514; 
            &#1488;&#1497;&#1512;&#1493;&#1506;&#1497; &#1496;&#1492;&#1512;&#1492;: %s</span>
                   <table cellspacing="0" cellpadding="5"><tr><td>#</td><td>&#1505;&#1493;&#1490;</td>
                   <td>&#1514;&#1488;&#1512;&#1497;&#1498;</td>
                   <td>&#1492;&#1506;&#1512;&#1493;&#1514;</td></tr>''' % filename
            for taharaEvent in taharaEvents:
                count += 1
                html += '<tr style="background-color:' + ('#ffffff;' if count % 2 else '#f1f1f1;') + '">'
                html += '''<td width="20">%s</td><td>%s</td><td>%s</td><td width="460">%s</td></tr>
                ''' % (count, taharaEvent.type, taharaEvent.jewish_date, taharaEvent.notes or '&nbsp;')
            html += '</table>'
    html += '</body></html>'
    return html
