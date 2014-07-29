import xml.dom.minidom

class EntryLists():
    def __init__(self, xmlString):
        self.entryList = []
        self.kavuahList = []

        docNode = xml.dom.minidom.parseString(xmlString.encode( "utf-8" ))

        for entry in docNode.getElementsByTagName('Entry'):
            self.entryList.append(Entry(entry))

        for kavuah in docNode.getElementsByTagName('Kavuah'):
            self.kavuahList.append(Kavuah(kavuah))

class Entry():
      def __init__(self, xmlNode):
          isInvisibleNodes = xmlNode.getElementsByTagName('IsInvisible')
          self.isInvisible = isInvisibleNodes.length and isInvisibleNodes[0].childNodes[0].data == 'True'
          self.date = xmlNode.getElementsByTagName('Date')[0].childNodes[0].data
          self.dn = ((xmlNode.getElementsByTagName('DN')[0].childNodes[0].data == '1' and 'Day') or 'Night')
          notesNode = xmlNode.getElementsByTagName('Notes')[0].childNodes
          self.notes = (notesNode.length and notesNode[0].data) or None

class Kavuah():
      def __init__(self, xmlNode):
          self.active = xmlNode.getElementsByTagName('Active')[0].childNodes[0].data == 'true'
          self.type = xmlNode.getElementsByTagName('KavuahType')[0].childNodes[0].data
          self.dn = xmlNode.getElementsByTagName('DayNight')[0].childNodes[0].data
          self.number = xmlNode.getElementsByTagName('Number')[0].childNodes[0].data
          self.cancels = xmlNode.getElementsByTagName('CancelsOnahBeinanis')[0].childNodes[0].data == 'true'
          hasNotesNode = xmlNode.getElementsByTagName('Notes').length and xmlNode.getElementsByTagName('Notes')[0].hasChildNodes()
          self.notes = (hasNotesNode and xmlNode.getElementsByTagName('Notes')[0].childNodes[0].data) or None