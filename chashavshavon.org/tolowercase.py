import webapp2

class LowerCaseRedirecter(webapp2.RequestHandler):
    def get(self, path):
        self.redirect('/static/%s' % (path.lower(),))

application = webapp2.WSGIApplication ([('/static/(.*)', LowerCaseRedirecter)])