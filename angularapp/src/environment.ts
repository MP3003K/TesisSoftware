export const isProxied = true;
export const isSSL = false
export const PROTOCOL = isSSL ? 'https':'http'
export const HOST = 'localhost'
export const PORT = '80'
export const baseURL = isProxied ? '' : `${PROTOCOL}://${HOST}:${PORT}`

