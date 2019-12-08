function FGetTableSize(_Table)
    local VResult = 0

    for Key, Value in pairs(_Table) do
        VResult = VResult + 1
    end

    return VResult
end

function FIsExistItemInTable(_Table, _Value)
    for Key, Value in pairs(_Table) do
        if (Value == _Value) then
            return true
        end
    end

    return false
end

local VTrimFind = string.find
local VTrimSub = string.sub

function FTrimString(_Value)
    local i1,i2 = VTrimFind(_Value,'^%s*')
   if i2 >= i1 then
    _Value = VTrimSub(_Value,i2+1)
   end
   local i1,i2 = VTrimFind(_Value,'%s*$')
   if i2 >= i1 then
    _Value = VTrimSub(_Value,1,i1-1)
   end
   return _Value
end

function FStart (_BaseUrl, _Category, _PagesRange, _Content)
    local VPageNumber = 1
    local VPageAddress = _BaseUrl .. "/" .. _Category .. "/?" .. "PAGEN_1="
    local VIsFinished = false

    local VMaxPages = 0
    local VFromPage = 1
    local VToPage = 1

    local VResultTable = {}

    local VCounter = 0

    local VWaitingTimer = 1750

    SnatcherFunctional.FAddEvent("Начало инициализации скрипта")

    SnatcherFunctional.FAddEvent("Загрузка начальной страницы - " .. _BaseUrl .. "/" .. _Category .. "/")

    SnatcherFunctional.FLoadWebSite(_BaseUrl .. "/" .. _Category .. "/")

    SnatcherFunctional.FAddEvent("Ожидание - " .. tostring(VWaitingTimer))

    SnatcherFunctional.FWaiting(VWaitingTimer)

    SnatcherFunctional.FAddEvent("Попытка получить последнюю кнопку для переключения страницы (при условии что страница содержит разделитель кнопок)")

    SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".pagination_button--etc", 0, false, false)

    if (SnatcherFunctional.FIsEmptyQueryDataCollection()) then
        SnatcherFunctional.FAddEvent("Разделитель не обнаружен")

        local VPaginationButtons = {}

        SnatcherFunctional.FAddEvent("Попытка вытащить последнюю кнопку для переключения страницы (при условии что разделитель не обнаружен)")

        SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".pagination_button", 1, false, false)

        SnatcherFunctional.FAddEvent("Кнопка получена")

        VPaginationButtons = SnatcherFunctional.FGetQueryDataCollection()

        SnatcherFunctional.FAddEvent("Максимальное количество страниц с товаром - " .. VPaginationButtons[FGetTableSize(VPaginationButtons) - 1])

        VMaxPages = tonumber(VPaginationButtons[FGetTableSize(VPaginationButtons) - 1])
    else
        SnatcherFunctional.FAddEvent("Разделитель обнаружен")

        SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".pagination_button--etc + .pagination_button", 0, false, false)

        SnatcherFunctional.FAddEvent("Кнопка получена")

        SnatcherFunctional.FAddEvent("Максимальное количество страниц с товаром - " .. SnatcherFunctional.FGetQueryData())

        VMaxPages = tonumber(SnatcherFunctional.FGetQueryData())
    end

    SnatcherFunctional.FAddEvent("Настройка диапазона парсинга страниц")

    if (FGetTableSize(_PagesRange) == 1) then
        VToPage = _PagesRange[1]
    elseif (FGetTableSize(_PagesRange) == 2) then
        VFromPage = _PagesRange[1]
        VToPage = _PagesRange[2]
    else
        VToPage = VMaxPages
    end

    if(VToPage > VMaxPages) then
        SnatcherFunctional.FDebugMessageBox_String("Неправильно указан диапазон страниц!\nКоличество страниц на веб-сайте меньше чем указанное.")
        return VResultTable
    end

    VPageNumber = VFromPage

    SnatcherFunctional.FSetMaxBarValue(VToPage)

    for c = VPageNumber,VToPage,1 do
        SnatcherFunctional.FSetProgressBarValue(c)

        SnatcherFunctional.FAddEvent("Загрузка страницы категории - " .. VPageAddress .. VPageNumber)

        SnatcherFunctional.FLoadWebSite(VPageAddress .. VPageNumber)

        SnatcherFunctional.FAddEvent("Ожидание - " .. tostring(VWaitingTimer))

        SnatcherFunctional.FWaiting(VWaitingTimer)

        SnatcherFunctional.FAddEvent("Получение списка товаров на странице")

        SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".catalog_item_left > .catalog_item_text > a", 1, true)

        local VPosts = SnatcherFunctional.FGetQueryDataCollection()

        SnatcherFunctional.FAddEvent("Обработка списка товаров на странице")

        for Key, Value in pairs(VPosts) do
            local VPostData = {}

            SnatcherFunctional.FAddEvent("Обработка товара #" .. Key .. " на странице #" .. tostring(VPageNumber))

            SnatcherFunctional.FAddEvent("Получение адреса страницы товара")

            local VPostAddress = SnatcherFunctional.FExecuteJavaScript("__GetLinkAddress", "function __GetLinkAddress() { var VBuffer = document.createElement('div'); VBuffer.innerHTML = '" .. Value .. "'; return VBuffer.children[0].getAttribute('href'); }", 0)

            SnatcherFunctional.FAddEvent("Загрузка страницы товара")
            
            SnatcherFunctional.FLoadWebSite(_BaseUrl .. VPostAddress)

            SnatcherFunctional.FAddEvent("Ожидание - " .. tostring(VWaitingTimer))

            SnatcherFunctional.FWaiting(VWaitingTimer)

            SnatcherFunctional.FAddEvent("Получение наименования товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".product > h1", 0, false, true)
            local VPostTitle = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение рейтинга товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".product-rating > .raiting > .raiting-votes", 0, true, false )
            local VPostRaiting = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение количества отзывов о товаре")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".catalog_item_rating_rev-count", 0, false, true )
            local VPostAmountReviews = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение цены товара")
            
            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".price-block_price", 0, false, true )
            local VPostPrice = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение описания товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".product-collapse_drop", 0, false, true )
            local VPostDescription = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение артикула товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".product-head_right", 0, false, true )
            local VPostPartNumber = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение характеристик товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".description-more_table-row", 1, true, false)
            local VPostDetails = SnatcherFunctional.FGetQueryDataCollection()

            SnatcherFunctional.FAddEvent("Получение статуса доступности товара при заказе в интернет магазине")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".stock-list_item.head", 0, false, true )
            local VPostIsProductAvailableInWebShop = SnatcherFunctional.FGetQueryData()

            SnatcherFunctional.FAddEvent("Получение статуса доступности товара при заказе в розничном магазине")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".stock-list_item.green", 1, true, true )
            local VPostIsProductAvailableInRetailShop = SnatcherFunctional.FGetQueryDataCollection()

            SnatcherFunctional.FAddEvent("Получение вариантов доставки товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".b-delivery-types__item", 1, false, true)
            local VPostDeliveryOptions = SnatcherFunctional.FGetQueryDataCollection()

            SnatcherFunctional.FAddEvent("Получение детального описания товара")

            SnatcherFunctional.FExtractData(SnatcherFunctional.FGetWebSiteData(), ".description-more > div > div", 0, false, true )
            local VPostDetailedDescription = SnatcherFunctional.FGetQueryData()

            if (FIsExistItemInTable(_Content, "Title")) then
                SnatcherFunctional.FAddEvent("Наименование товара добавлено")

                VPostData["Title"] = VPostTitle
            end

            if (FIsExistItemInTable(_Content, "Rate")) then
                SnatcherFunctional.FAddEvent("Рейтинг товара добавлен")

                VPostData["Rate"] = SnatcherFunctional.FExecuteJavaScript("__GetRating", "function __GetRating() { var VBuffer = document.createElement('div'); VBuffer.innerHTML = '" .. VPostRaiting .. "'; var VRaitingWidth = parseInt(VBuffer.children[0].style.width, 10); if(VRaitingWidth <= 17) { return 1; } else if(VRaitingWidth <= 34) { return 2; } else if(VRaitingWidth <= 51) { return 3; } else if(VRaitingWidth <= 68) { return 4; } else if(VRaitingWidth <= 85) { return 5; } else { return 0; } }", 1)
            end

            if (FIsExistItemInTable(_Content, "AmountReviews")) then
                SnatcherFunctional.FAddEvent("Количество отзывов о товаре добавлено")

                VPostData["AmountReviews"] = VPostAmountReviews
            end

            if (FIsExistItemInTable(_Content, "Price")) then
                SnatcherFunctional.FAddEvent("Цена товара добавлена")

                VPostData["Price"] = VPostPrice
            end

            if (FIsExistItemInTable(_Content, "Description")) then
                SnatcherFunctional.FAddEvent("Описание товара добавлено")

                VPostData["Description"] = VPostDescription
            end

            if (FIsExistItemInTable(_Content, "PartNumber")) then
                SnatcherFunctional.FAddEvent("Артикул товара добавлен")

                VPostData["PartNumber"] = VPostPartNumber
            end

            SnatcherFunctional.FAddEvent("Формирование списка полей характеристики товара")

            local VOtherFieldsList = ""

            for Key, Value in pairs(VPostDetails) do
                SnatcherFunctional.FAddEvent("Получение пары ключ-значение (название-описание) поля характеристики товара")

                local VDetailItemTitle = SnatcherFunctional.FExecuteJavaScript("__GetDetailItemTitle", "function __GetDetailItemTitle() { var VBuffer = document.createElement('div'); VBuffer.innerHTML = '" .. Value .. "'; return VBuffer.children[0].children[0].innerText; }", 0)
                local VDetailItemContent = SnatcherFunctional.FExecuteJavaScript("__GetDetailItemContent", "function __GetDetailItemContent() { var VBuffer = document.createElement('div'); VBuffer.innerHTML = '" .. Value .. "'; return VBuffer.children[0].children[1].innerText; }", 0)
                
                SnatcherFunctional.FAddEvent("Удаление лишних пробелов в названии поля характеристики товара")

                VDetailItemTitle = FTrimString(VDetailItemTitle)
                
                if (VDetailItemTitle == "Тип:" and FIsExistItemInTable(_Content, "ProductType")) then
                    SnatcherFunctional.FAddEvent("Тип товара добавлен")

                    VPostData["ProductType"] = VDetailItemContent
                elseif (VDetailItemTitle == "Марка:" and FIsExistItemInTable(_Content, "Manufacturer")) then
                    SnatcherFunctional.FAddEvent("Марка товара добавлена")

                    VPostData["Manufacturer"] = VDetailItemContent
                elseif (VDetailItemTitle == "Страна производства:" and FIsExistItemInTable(_Content, "ManufacturerCountry")) then
                    SnatcherFunctional.FAddEvent("Страна производства товара добавлена")

                    VPostData["ManufacturerCountry"] = VDetailItemContent
                elseif (FIsExistItemInTable(_Content, "ParseOtherFields")) then
                    SnatcherFunctional.FAddEvent("Формирование списка дополнительных полей характеристики товара")

                    VOtherFieldsList = VOtherFieldsList .. VDetailItemTitle .. " " .. VDetailItemContent .. "   "
                end
            end

            if (string.len(VOtherFieldsList) > 0) then
                SnatcherFunctional.FAddEvent("Дополнительные поля характеристики товара добавлены")

                VPostData["ParseOtherFields"] = VOtherFieldsList
            end

            if (FIsExistItemInTable(_Content, "IsAvailableInWebShop")) then
                SnatcherFunctional.FAddEvent("Статус доступности при заказе товара в интернет магазине добавлен")

                VPostData["IsAvailableInWebShop"] = VPostIsProductAvailableInWebShop
            end

            SnatcherFunctional.FAddEvent("Формирование списка розничных магазинов для покупки")

            local VRetailShopsList = ""

            for Key, Value in pairs(VPostIsProductAvailableInRetailShop) do
                VRetailShopsList = VRetailShopsList .. Value .. "   "
            end

            if (string.len(VRetailShopsList) > 0) then
                SnatcherFunctional.FAddEvent("Статус доступности при заказе товара в розничном магазине добавлен")

                VPostData["IsAvailableInRetailShop"] = VRetailShopsList
            end

            SnatcherFunctional.FAddEvent("Формирование списка вариантов доставки товара")

            local VDeliveryOptionsList = ""

            for Key, Value in pairs(VPostDeliveryOptions) do
                VDeliveryOptionsList = VDeliveryOptionsList .. Value .. "   "
            end

            if (string.len(VDeliveryOptionsList) > 0) then
                SnatcherFunctional.FAddEvent("Варианты доставки товара добавлены")

                VPostData["DeliveryOptions"] = VDeliveryOptionsList
            end

            if (FIsExistItemInTable(_Content, "ParseDetailedDescription")) then
                SnatcherFunctional.FAddEvent("Детальное описание товара добавлено")

                VPostData["ParseDetailedDescription"] = VPostDetailedDescription
            end

            SnatcherFunctional.FAddEvent("Данные товара сформированы и добавлены в таблицу")

            VResultTable[VCounter] = VPostData

            VCounter = VCounter + 1
        end

        VPageNumber = VPageNumber + 1
    end

    SnatcherFunctional.FAddEvent("Скрипт закончил парсинг")

    return VResultTable
end
