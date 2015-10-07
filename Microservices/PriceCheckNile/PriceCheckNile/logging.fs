module Logging
open Marvel

let Log = Log.create __SOURCE_FILE__

let catchLogThrow (log:NLog.Logger) =
    AsyncArrow.catch
    >> AsyncArrow.logErrorsTo log
    >> AsyncArrow.throw

