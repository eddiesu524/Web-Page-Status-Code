# .Net Core Web API Try Web Page Status Code 
Use StatusCode to verify that the status of the HttpWebResponse is OK.

### Setting Example
- Setting_WebSite.json

```json
    {
        "Setting_WebSite": {
            "WebSiteSettings": [
                {
                    "WebSite": "GOOGLE",
                    "URL": "https://www.google.com",
                    "Desc": ""
                }
            ]
        }
    }
```

### Return Example

```json
    [
        {
            "webSiteName": "GOOGLE",
            "url": "https://www.google.com",
            "statusCode": "OK",
            "timeSpan_Second": "0.1279264",
            "msg": "OK"
        }
    ]
```