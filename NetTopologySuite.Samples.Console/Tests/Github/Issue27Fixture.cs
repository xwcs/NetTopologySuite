using GeoAPI.Geometries;
using NetTopologySuite.Algorithm.Distance;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NUnit.Framework;

namespace NetTopologySuite.Samples.Tests.Github
{
    [TestFixture]
    public class Issue27Fixture
    {
        internal const string Poly1Wkt = @"POLYGON ((447659.00088514 6232828.85568136, 447661.310889315 6232661.0118306, 447663.82381757 6232509.2309641, 447665.32016632 6232369.69959036, 447666.70195289 6232269.29960783, 447666.690152371 6232241.95127252, 447667.52798224 6232163.8253994, 447667.52798224 6232118.3170907, 447800.87791023 6232122.0212554, 447968.701300134 6232125.715481, 447968.63178764 6232132.83738432, 447962.290733373 6232134.33707906, 447957.64007719 6232141.30566063, 447959.97365113 6232150.62376395, 447968.37098475 6232156.30237139, 447968.345104083 6232160.45717706, 447967.56532022 6232193.9878832, 447967.550605288 6232195.33279361, 447966.94138956 6232218.80988487, 447963.840952101 6232227.33814896, 447966.94138956 6232234.30673053, 447966.941389556 6232251.01368731, 447966.179482514 6232320.65020981, 447961.523869854 6232327.30780043, 447966.049959616 6232332.48829994, 447963.998814869 6232519.95813469, 447961.523869854 6232524.9275755, 447961.523869854 6232528.79678793, 447963.87887844 6232530.92004396, 447962.832244316 6232626.57995612, 447960.311730742 6232628.08657778, 447958.761512014 6232637.42467704, 447961.870195314 6232645.17309987, 447961.870195314 6232714.50898591, 447961.74449003 6232725.998155, 447961.74449003 6232728.1148205, 447961.870195314 6232732.28578422, 447961.870195314 6232738.17416977, 447958.761512014 6232744.37290803, 447957.21129329 6232751.35148756, 447959.55311307 6232756.00054126, 447962.52161701 6232759.55981678, 447962.480387792 6232764.70876873, 447962.480387792 6232764.75875855, 447962.480387792 6232764.81874634, 447962.474320096 6232767.11412815, 447962.47214195 6232767.65816838, 447962.43915857 6232769.02788958, 447962.43091273 6232770.94749885, 447962.404854601 6232773.89637429, 447960.181600783 6232833.47957667, 447880.43723736 6232832.74052964, 447659.00088514 6232828.85568136), (447793.6459701 6232302.2387078, 447794.35152527 6232308.2359268, 447797.87930115 6232318.4664768, 447800.70152185 6232328.3442493, 447801.40707702 6232338.9275769, 447804.93485289 6232344.9247959, 447810.2265167 6232345.9831286, 447816.92929086 6232345.2775735, 447818.34040121 6232338.2220217, 447817.63484603 6232335.399801, 447818.6931788 6232327.2859165, 447814.81262534 6232313.5275906, 447813.40151499 6232309.9998147, 447810.2265167 6232302.2387078, 447808.10985118 6232294.4776009, 447802.11263219 6232290.949825, 447796.11541321 6232293.4192681, 447793.6459701 6232302.2387078), (447811.637626875 6232500.8524926, 447811.284849291 6232518.49137189, 447817.63484586 6232530.8385875, 447827.51261831 6232528.0163668, 447827.86539589 6232514.2580409, 447829.27650624 6232500.499715, 447829.100117447 6232492.38583046, 447826.80706313 6232485.68305626, 447817.28206827 6232483.5663908, 447811.63762688 6232490.2691649, 447811.99040447 6232491.3274977, 447811.637626875 6232500.8524926), (447840.91816652 6232640.9051964, 447842.32927687 6232647.9607481, 447849.03205103 6232658.1912981, 447852.5598269 6232673.713512, 447857.85149071 6232678.6523982, 447866.3181528 6232676.1829551, 447868.08204074 6232666.6579602, 447865.61259763 6232653.6051895, 447857.49871312 6232643.0218619, 447850.79593896 6232637.3774205, 447844.09316481 6232637.3774205, 447840.91816652 6232640.9051964))";
        internal const string Poly2Wkt = @"POLYGON ((447961.82530680706 6232833.4948004484, 448204.281525929 6232835.23441344, 448204.57013048 6232775.91648743, 448204.941193473 6232595.93312252, 448206.903704415 6232553.43177355, 448208.940427957 6232208.61196062, 448207.43143845 6232131.64762649, 448207.151079744 6232127.09855244, 448200.570895996 6232127.09855244, 448072.32327966 6232124.77902458, 447968.969867253 6232125.72883125, 447968.63178764074 6232132.8373842416, 447968.76372115 6232119.32013572, 447968.49160828 6232114.25116749, 447701.12010698 6232110.07201814, 447668.35112175 6232109.56212193, 447380.24956789 6232105.06303771, 447288.81964634 6232103.63332872, 447125.30264852 6232100.09980099, 447113.48001323 6232100.09404913, 447102.356369275 6232097.76092161, 447091.23272532 6232092.60557339, 447090.92762908 6232096.15485094, 447088.61879267 6232193.83496844, 447093.075671513 6232288.55415544, 447097.532550355 6232374.80668035, 447105.51720507 6232525.97028132, 447106.44630804 6232553.66172674, 447106.444782067 6232576.42223989, 447105.73770092 6232602.71052892, 447102.912428275 6232651.7593311, 447102.906324384 6232658.84031794, 447127.881994617 6232662.53951074, 447240.346506496 6232681.76091738, 447312.562513589 6232699.76918549, 447323.747998083 6232738.59133125, 447328.24808055 6232761.29641657, 447313.345395715 6232783.64872429, 447287.773357094 6232832.58667079, 447287.429252421 6232838.7403911, 447341.014874881 6232839.19923826, 447387.106122124 6232836.75807906, 447477.148010195 6232835.54733156, 447582.712112107 6232835.04213924, 447687.92343643 6232834.36055812, 447701.01800881 6232837.90032102, 447751.48610462 6232836.49520419, 447790.31254005 6232832.97342183, 447962.34988663 6232835.08226668, 447962.3493503 6232833.49856044, 447961.82530680706 6232833.4948004484), (447964.739749129 6232531.69619776, 447964.640798997 6232541.6941627, 447964.640798997 6232541.74415253, 447964.640798997 6232541.82413625, 447964.59956982225 6232545.5433750348, 447964.739749129 6232531.69619776), (447964.739749129 6232531.69619776, 447961.52386985 6232528.79678793, 447961.52386985 6232524.9275755, 447964.87992848 6232518.18894712, 447964.9211577 6232514.59967771, 447966.67752254 6232333.20659975, 447961.52386985 6232327.30780043, 447966.80945605 6232319.74933893, 447966.94138956 6232306.28208015, 447966.94138956 6232234.30673053, 447963.8409521 6232227.33814896, 447966.94138956 6232218.80988487, 447968.310199709 6232166.0606218679, 447968.310199709 6232223.21898741, 447971.410637165 6232225.54851324, 447971.410637165 6232229.8076463, 447969.860418437 6232232.90701544, 447967.642286321 6232235.12656365, 447967.634040477 6232235.81642323, 447967.634040477 6232235.85641509, 447967.551582034 6232243.58484199, 447965.861183953 6232518.31892067, 447969.340930246 6232519.4786846, 447972.820676539 6232524.90757957, 447970.091302077 6232529.55663326, 447964.113064963 6232530.32647656, 447964.154294184 6232531.16630562, 447964.739749129 6232531.69619776), (447962.480387792 6232764.81874634, 447962.472141948 6232767.9381114, 447962.463896103 6232768.64796691, 447962.340208439 6232781.45536001, 447962.340208439 6232781.53534373, 447962.33547563286 6232781.7476666747, 447962.43091273 6232770.94749885, 447962.43915857 6232769.02788958, 447962.47214195 6232767.65816838, 447962.480387792 6232764.81874634), (447964.162540028 6232591.7239794973, 447964.162540028 6232592.05391212, 447963.840952101 6232626.596881, 447968.920392187 6232633.57546053, 447968.161774512 6232642.21370224, 447962.900925851 6232646.97273355, 447962.752500654 6232738.90402121, 447969.29145518 6232744.51287954, 447966.990864622 6232752.65122301, 447962.950400917 6232757.84016681, 447962.480387792 6232764.70876873, 447962.52161701 6232759.55981678, 447959.55311307 6232756.00054126, 447957.21129329 6232751.35148756, 447958.76151201 6232744.37290803, 447961.87019531 6232738.17416977, 447961.87019531 6232645.17309987, 447958.76151201 6232637.42467704, 447960.31173074 6232628.08657778, 447963.8409521 6232625.97700718, 447964.162540028 6232591.7239794973), (447968.37098474993 6232156.3023713976, 447968.400903996 6232156.33260194, 447968.310199712 6232166.06062151, 447968.37098474993 6232156.3023713976), (447968.631787637 6232132.8373843208, 447968.631787637 6232132.85738025, 447968.631787637 6232132.87737618, 447973.34016473 6232133.31728664, 447978.741192744 6232143.00531467, 447975.673738666 6232150.87371308, 447968.359674775 6232153.07326536, 447968.351428931 6232156.28261211, 447968.37098472653 6232156.3023713743, 447959.97365113 6232150.62376395, 447957.64007719 6232141.30566063, 447962.29073337 6232134.33707906, 447968.631787637 6232132.8373843208), (447407.00173461 6232812.0023283, 447414.93923033 6232807.2398309, 447428.16838985 6232807.2398309, 447431.34338814 6232817.2939922, 447427.992001057 6232819.41065768, 447410.882288075 6232819.76343526, 447407.00173461 6232812.0023283))";

        //
        // NOTE: These WKB's (both Big- and LittleEndian) are created with JTS run in Eclipse with jdk compliance set to 1.7
        //

        //BigEndian
        //internal const string Poly1Wkb = "00000000030000000400000035411B52AC00E808BD4157C6BF36C37BC0411B52B53E59C4C24157C69540C1D522411B52BF4B96D5444157C66F4EC81DA6411B52C547D9AE074157C64C6CC616A5411B52CACECCBD084157C633532CC652411B52CAC2B74D9B4157C62C7CE1A623411B52CE1CA760574157C618F4D35801411B52CE1CA760574157C60D944B36CB411B54E382FAE63B4157C60E815C3F9C411B5782CE219F514157C60F6DCA70D2411B578286F356CF4157C6113597B467411B576929B602644157C6119592B40D411B57568F7065184157C613538FF19A411B575FE504CD444157C615A7EBBFA1411B57817BE36D224157C617135A0D88411B57816162F6F94157C6181D426392411B577E42E34DC34157C6207F397A75411B577E33D1DF644157C620D54C7D92411B577BC3FB9FF44157C626B3D5275A411B576F5D228C2D4157C628D5A43B89411B577BC3FB9FF44157C62A93A17917411B577BC3FB9FAF4157C62EC0E040BD411B5778B7CA439F4157C640299D099B411B5766187156C94157C641D3B30093411B577833289D134157C6431F404E64411B576FFEC953344157C671FD52142A411B5766187156C94157C6733B5D65A1411B5766187156C94157C67432FE92CD411B576F83F8B5B44157C674BAE20010411B576B5437DA9E4157C68CA51E0046411B57613F3657F84157C68D058A7D87411B575B0BC9CE2F4157C68F5B2DE89C411B57677B147AFB4157C6914B14117A411B57677B147AFB4157C6A2A09339A3411B5766FA5B982C4157C6A57FE1C582411B5766FA5B982C4157C6A607593815411B57677B147AFB4157C6A7124A49E6411B57677B147AFB4157C6A88B2598F7411B575B0BC9CE2F4157C6AA17DDB9A4411B5754D85D44AA4157C6ABD67EC5AE411B575E366345CB4157C6AD0008DE35411B576A1622C4FC4157C6ADE3D409C2411B5769EBEAC7004157C6AF2D5C7785411B5769EBEAC7004157C6AF308F8005411B5769EBEAC7004157C6AF3466570D411B5769E5B42AD14157C6AFC74DE028411B5769E3792DE94157C6AFEA1F6E45411B5769C1B2C8C04157C6B041C8F160411B5769B9412FCB4157C6B0BCA3D237411B57699E92345C4157C6B1795E3245411B5760B9F58E404157C6C05EB16258411B5621BFBB26874157C6C02F64D66E411B52AC00E808BD4157C6BF36C37BC000000012411B54C695792F974157C63B8F46FD15411B54C967F63D894157C63D0F196CB9411B54D78467854A4157C63F9DDAC182411B54E2CE5BBE6D4157C64216082E37411B54E5A0D8CC5F4157C644BB5D6B81411B54F3BD4A13744157C6463B2FDB24411B5508E7F3FE6A4157C6467EEB9438411B5523B79805F14157C64651C3C3A4411B55295C9222824157C6448E359A81411B55268A1513E34157C643D99656EF411B552AC5D0A9D14157C641D24C74B8411B551B4020DB6D4157C63E61C40B5D411B55159B26BEDC4157C63D7FFCF6CC411B5508E7F3FE6A4157C63B8F46FD15411B5500707CD3E64157C6399E91035E411B54E87355DA524157C638BCC9EECC411B54D0762EE16A4157C6395AD549DE411B54C695792F974157C63B8F46FD150000000C411B550E8CEE0F3D4157C66D368F3D1F411B550D23AF88554157C6719F72A315411B55268A15087B4157C674B5AB6AE8411B554E0CEBD0734157C674010C2756411B554F762A57174157C6709083BDFB411B55551B2473A84157C66D1FFB54A0411B5554668530224157C66B18B1723E411B554B3A6EC1D54157C6696BB7319B411B552520D6812C4157C668E43FBF33411B550E8CEE0F934157C66A9139FF6B411B550FF62C96E24157C66AD4F5B8EA411B550E8CEE0F3D4157C66D368F3D1F0000000C411B5583AC33D81F4157C69039EEBCE2411B5589512DF4B04157C691FD7CE599411B55A420D1FC374157C6948C3E3A63411B55B23D43434C4157C6986DAA2E3C411B55C767ED2E414157C699A9C0E461411B55E945C9D8FD4157C6990BB5894F411B55F054027CDD4157C696AA1C0519411B55E6734CCB0A4157C69366BB6CBE411B55C5FEAEA6F24157C690C1662F74411B55AB2F0A9F6B4157C68F5827A850411B55905F6698904157C68F5827A850411B5583AC33D81F4157C69039EEBCE2";
        //internal const string Poly2Wkb = "00000000030000000800000033411B57674D1D3A464157C6C05FAACF80411B5B31204855484157C6C0CF00A13B411B5B3247D048D84157C6B1FAA7BAE5411B5B33C3C838C74157C684FBB84785411B5B3B9D64B0AF4157C67A5BA22D87411B5B43C2FF8BDE4157C624272A5CE0411B5B3DB9CB00444157C610E972B661411B5B3C9AB4A5FE4157C60FC64EAEE5411B5B224898F5C14157C60FC64EAEE5411B59214B09D2BD4157C60F31DB89E9411B5783E124E1944157C60F6EA52BD4411B578286F356DC4157C6113597B413411B57830E0CEACA4157C60DD47D1A88411B5781F76829344157C60C901320CF411B53547AFD52FC4157C60B849BF1F9411B52D1678C75C54157C60B63F9CE42411B4E50FF8EB9974157C60A4408CF52411B4CE347515EC24157C609E888752F411B4A5535E97E5E4157C609066323B1411B4A25EB8896924157C6090604E6A4411B49F96CEC11364157C60870B2F08D411B49CCEE4F8BD94157C60726C1B6E4411B49CBB5E465C64157C60809E913EB411B49C279A4C9234157C62075701F78411B49D44D7CD5464157C63823774861411B49E62154E1594157C64DB3A0A69E411B4A06119E34B44157C6737E1916D2411B4A09C904F98F4157C67A6A59BB1D411B4A09C774F33E4157C6801B05FA76411B4A06F367DEB74157C686AD794E4B411B49FBA65399044157C692F098E178411B49FBA0137FD34157C694B5C7C4E6411B4A5F872998CD4157C695A287580E411B4C2162D299514157C69A70B2DED0411B4D4240038FF14157C69EF13A55C7411B4D6EFDF335A04157C6A8A5D85F07411B4D80FE08D3E44157C6AE52F87D35411B4D4561AF6A104157C6B3E984B2E2411B4CDF17EAEC0B4157C6C0258C03A4411B4CDDB78DF2584157C6C1AF62915A411B4DB40F3B5C5E4157C6C1CCC051D5411B4E6C6CAB47304157C6C130845E09411B4FD4978FFC0C4157C6C0E3077AF4411B517AD933EA8B4157C6C0C2B268C8411B531FB19951CB4157C6C09713625D411B53541270E6C84157C6C1799EDC0E411B541DF1C568D54157C6C11FB16CEA411B54B9400A7FB64157C6C03E4C8B13411B57696648AE454157C6C0C543DB77411B576965BC15C54157C6C05FE86A0C411B57674D1D3A464157C6C05FAACF8000000006411B5772F580CBB14157C674EC8E810D411B5772902D9CBE4157C6776C6D2964411B5772902D9CBE4157C6776FA031EF411B5772902D9CBE4157C67774BEA5F8411B577265F5A1A94157C67862C6A815411B5772F580CBB14157C674EC8E810D0000001C411B5772F580CBB14157C674EC8E810D411B5766187156844157C67432FE92CD411B5766187156844157C6733B5D65A1411B5773850BF8B24157C6718C17B5A9411B5773AF43F6D04157C670A6611E9E411B577AB5C877FE4157C6434D38EE28411B5766187156844157C641D3B30093411B577B3CE20BF94157C63FEFF52B45411B577BC3FB9FF44157C63C920D99E7411B577BC3FB9FF44157C62A93A17917411B576F5D228C1C4157C628D5A43B89411B577BC3FB9FF44157C626B3D5275A411B57813DA4FE164157C61983E13A8B411B57813DA4FE164157C627CE03E3C5411B578DA47E11A94157C628631AD747411B578DA47E11A94157C62973B07A1B411B5787711187DF4157C62A3A0C8A7D411B577E91B3815D4157C62AC8199E6C411B577E8941E8244157C62AF4404738411B577E8941E8244157C62AF6CF813D411B577E34D1EBB44157C62CE56E0D19411B577771DA34C84157C6719469323E411B57855D1CD1834157C671DEA2C4BC411B5793485F6E3E4157C6733A15C89F411B57885D7E4AAB4157C674639FE11C411B577073C74D3A4157C67494E4FDF1411B57709DFF4B694157C674CAA4C054411B5772F580CBB14157C674EC8E810D0000000A411B5769EBEAC7004157C6AF3466570D411B5769E3792DC74157C6AFFC0A0466411B5769DB07947D4157C6B029784A34411B57695C5F99DD4157C6B35D249E50411B57695C5F99DD4157C6B362431259411B57695786EC9F4157C6B36FD9C553411B5769B9412FCB4157C6B0BCA3D237411B5769C1B2C8C04157C6B041C8F160411B5769E3792DE94157C6AFEA1F6E45411B5769EBEAC7004157C6AF3466570D00000015411B5770A670E4A24157C683EE55AE1A411B5770A670E4A24157C68403734BD2411B576F5D228C2D4157C68CA6334C5E411B5783AE7B4A1B4157C68E64D45867411B5780A5A837B94157C6908DAD4C29411B576B9A8C4E694157C691BE414438411B576B028F880D4157C6A8B9DB7BC7411B57852A733A094157C6AA20D304B5411B577BF6A537294157C6AC29ADA347411B576BCD35E5E24157C6AD75C54B03411B5769EBEAC7004157C6AF2D5C7785411B576A1622C4FC4157C6ADE3D409C2411B575E366345CB4157C6AD0008DE35411B5754D85D44AA4157C6ABD67EC5AE411B575B0BC9CDEA4157C6AA17DDB9A4411B57677B147AB64157C6A88B2598F7411B57677B147AB64157C6914B14117A411B575B0BC9CDEA4157C68F5B2DE89C411B57613F3657D64157C68D058A7D87411B576F5D228C1C4157C68C7E874920411B5770A670E4A24157C683EE55AE1A00000004411B57817BE36D214157C617135A0D90411B57819A8693BF4157C617154959A6411B57813DA4FE494157C61983E1390B411B57817BE36D214157C617135A0D900000000D411B578286F3569C4157C6113597B468411B578286F3569C4157C61136DF5169411B578286F3569C4157C6113826EE6C411B57955C54249A4157C611544E6CA0411B57AAF6FB3B0E4157C613C0571357411B579EB1E88C824157C615B7EAEA44411B5781704E958F4157C61644B06131411B578167DCFC564157C6171216511A411B57817BE36B8F4157C617135A0D77411B575FE504CD444157C615A7EBBFA1411B57568F7065184157C613538FF19A411B576929B602304157C6119592B40D411B578286F3569C4157C6113597B46800000007411B4EBC01C6B7B54157C6BB00262599411B4EDBC1C5987B4157C6B9CF5963B4411B4F10AC6E638B4157C6B9CF5963B4411B4F1D5FA123FD4157C6BC52D0C4A9411B4F0FF7CF20064157C6BCDA483726411B4ECB8776866F4157C6BCF0DC1F91411B4EBC01C6B7B54157C6BB00262599";
        //LittleEndian
        internal const string Poly1Wkb = "01030000000400000035000000BD08E800AC521B41C07BC336BFC65741C2C4593EB5521B4122D5C14095C6574144D5964BBF521B41A61DC84E6FC6574107AED947C5521B41A516C66C4CC6574108BDCCCECA521B4152C62C5333C657419B4DB7C2CA521B4123A6E17C2CC657415760A71CCE521B410158D3F418C657415760A71CCE521B41CB364B940DC657413BE6FA82E3541B419C3F5C810EC65741519F21CE82571B41D270CA6D0FC65741CF56F38682571B4167B4973511C657416402B62969571B410DB4929511C657411865708F56571B419AF18F5313C6574144CD04E55F571B41A1BFEBA715C65741226DE37B81571B41880D5A1317C65741F9F6626181571B419263421D18C65741C34DE3427E571B41757A397F20C6574164DFD1337E571B41927D4CD520C65741F49FFBC37B571B415A27D5B326C657412D8C225D6F571B41893BA4D528C65741F49FFBC37B571B411779A1932AC65741AF9FFBC37B571B41BD40E0C02EC657419F43CAB778571B419B099D2940C65741C956711866571B419300B3D341C65741139D283378571B41644E401F43C657413453C9FE6F571B412A1452FD71C65741C956711866571B41A1655D3B73C65741C956711866571B41CD92FE3274C65741B4B5F8836F571B411000E2BA74C657419EDA37546B571B4146001EA58CC65741F857363F61571B41877D8A058DC657412FCEC90B5B571B419CE82D5B8FC65741FB7A147B67571B417A11144B91C65741FB7A147B67571B41A33993A0A2C657412C985BFA66571B4182C5E17FA5C657412C985BFA66571B4115385907A6C65741FB7A147B67571B41E6494A12A7C65741FB7A147B67571B41F798258BA8C657412FCEC90B5B571B41A4B9DD17AAC65741AA445DD854571B41AEC57ED6ABC65741CB4563365E571B4135DE0800ADC65741FCC422166A571B41C209D4E3ADC6574100C7EAEB69571B4185775C2DAFC6574100C7EAEB69571B4105808F30AFC6574100C7EAEB69571B410D576634AFC65741D12AB4E569571B4128E04DC7AFC65741E92D79E369571B41456E1FEAAFC65741C0C8B2C169571B4160F1C841B0C65741CB2F41B969571B4137D2A3BCB0C657415C34929E69571B4145325E79B1C65741408EF5B960571B415862B15EC0C657418726BBBF21561B416ED6642FC0C65741BD08E800AC521B41C07BC336BFC6574112000000972F7995C6541B4115FD468F3BC65741893DF667C9541B41B96C190F3DC657414A856784D7541B4182C1DA9D3FC657416DBE5BCEE2541B41372E081642C657415FCCD8A0E5541B41816B5DBB44C6574174134ABDF3541B4124DB2F3B46C657416AFEF3E708551B413894EB7E46C65741F10598B723551B41A4C3C35146C657418222925C29551B41819A358E44C65741E313158A26551B41EF5696D943C65741D1A9D0C52A551B41B8744CD241C657416DDB20401B551B415D0BC4613EC65741DCBE269B15551B41CCF6FC7F3DC657416AFEF3E708551B4115FD468F3BC65741E6D37C7000551B415E03919E39C6574152DA5573E8541B41CCEEC9BC38C657416AE12E76D0541B41DE49D55A39C65741972F7995C6541B4115FD468F3BC657410C0000003D0FEE8C0E551B411F3D8F366DC657415588AF230D551B4115A3729F71C657417B08158A26551B41E86AABB574C6574173D0EB0C4E551B4156270C0174C6574117572A764F551B41FBBD839070C65741A873241B55551B41A054FB1F6DC657412230856654551B413E72B1186BC65741D5C16E3A4B551B419B31B76B69C657412C81D62025551B4133BF3FE468C65741930FEE8C0E551B416BFF39916AC65741E2962CF60F551B41EAB8F5D46AC657413D0FEE8C0E551B411F3D8F366DC657410C0000001FD833AC83551B41E2BCEE3990C65741B0F42D5189551B4199E57CFD91C6574137FCD120A4551B41633A3E8C94C657414C43433DB2551B413C2EAA6D98C65741412EED67C7551B4161E4C0A999C65741FDD8C945E9551B414F89B50B99C65741DD7C0254F0551B4119051CAA96C657410ACB4C73E6551B41BE6CBB6693C65741F2A6AEFEC5551B41742F66C190C657416B9F0A2FAB551B4150A827588FC657419098665F90551B4150A827588FC657411FD833AC83551B41E2BCEE3990C65741";
        internal const string Poly2Wkb = "01030000000800000033000000463A1D4D67571B4180CFAA5FC0C6574148554820315B1B413BA100CFC0C65741D848D047325B1B41E5BAA7FAB1C65741C738C8C3335B1B418547B8FB84C65741AFB0649D3B5B1B41872DA25B7AC65741DE8BFFC2435B1B41E05C2A2724C657414400CBB93D5B1B4161B672E910C65741FEA5B49A3C5B1B41E5AE4EC60FC65741C1F59848225B1B41E5AE4EC60FC65741BDD2094B21591B41E989DB310FC6574194E124E183571B41D42BA56E0FC65741DC56F38682571B4113B4973511C65741CAEA0C0E83571B41881A7DD40DC65741342968F781571B41CF2013900CC65741FC52FD7A54531B41F9F19B840BC65741C5758C67D1521B4142CEF9630BC6574197B98EFF504E1B4152CF08440AC65741C25E5147E34C1B412F7588E809C657415E7EE935554A1B41B123630609C65741929688EB254A1B41A4E6040609C657413611EC6CF9491B418DF0B27008C65741D98B4FEECC491B41E4B6C12607C65741C665E4B5CB491B41EB13E90908C6574123C9A479C2491B41781F707520C6574146D57C4DD4491B416148772338C6574159E15421E6491B419EA6A0B34DC65741B4349E11064A1B41D216197E73C657418FF904C9094A1B411DBB596A7AC657413EF374C7094A1B4176FA051B80C65741B7DE67F3064A1B414B4E79AD86C65741049953A6FB491B4178E198F092C65741D37F13A0FB491B41E6C4C7B594C65741CD9829875F4A1B410E5887A295C657415199D262214C1B41D0DEB2709AC65741F18F0340424D1B41C7553AF19EC65741A035F3FD6E4D1B41075FD8A5A8C65741E4D308FE804D1B41357DF852AEC65741106AAF61454D1B41E2B284E9B3C657410BECEA17DF4C1B41A4038C25C0C6574158F28DB7DD4C1B415A9162AFC1C657415E5C3B0FB44D1B41D551C0CCC1C657413047AB6C6C4E1B41095E8430C1C657410CFC8F97D44F1B41F47A07E3C0C657418BEA33D97A511B41C868B2C2C0C65741CB5199B11F531B415D621397C0C65741C8E6701254531B410EDC9E79C1C65741D568C5F11D541B41EA6CB11FC1C65741B67F0A40B9541B41138B4C3EC0C6574145AE486669571B4177DB43C5C0C65741C515BC6569571B410C6AE85FC0C65741463A1D4D67571B4180CFAA5FC0C6574106000000B1CB80F572571B410D818EEC74C65741BE9C2D9072571B4164296D6C77C65741BE9C2D9072571B41EF31A06F77C65741BE9C2D9072571B41F8A5BE7477C65741A9A1F56572571B4115A8C66278C65741B1CB80F572571B410D818EEC74C657411C000000B1CB80F572571B410D818EEC74C657418456711866571B41CD92FE3274C657418456711866571B41A1655D3B73C65741B2F80B8573571B41A9B5178C71C65741D0F643AF73571B419E1E61A670C65741FE77C8B57A571B4128EE384D43C657418456711866571B419300B3D341C65741F90BE23C7B571B41452BF5EF3FC65741F49FFBC37B571B41E7990D923CC65741F49FFBC37B571B411779A1932AC657411C8C225D6F571B41893BA4D528C65741F49FFBC37B571B415A27D5B326C6574116FEA43D81571B418B3AE18319C6574116FEA43D81571B41C5E303CE27C65741A9117EA48D571B4147D71A6328C65741A9117EA48D571B411B7AB07329C65741DF87117187571B417D8A0C3A2AC657415D81B3917E571B416C9E19C82AC6574124E841897E571B41384740F42AC6574124E841897E571B413D81CFF62AC65741B4EBD1347E571B41190D6EE52CC65741C834DA7177571B413E32699471C6574183D11C5D85571B41BCC4A2DE71C657413E6E5F4893571B419FC8153A73C65741AB4A7E5D88571B411CE19F6374C657413A4DC77370571B41F1FDE49474C65741694BFF9D70571B4154C0A4CA74C65741B1CB80F572571B410D818EEC74C657410A00000000C7EAEB69571B410D576634AFC65741C72D79E369571B4166040AFCAFC657417D9407DB69571B41344A7829B0C65741DD995F5C69571B41509E245DB3C65741DD995F5C69571B4159124362B3C657419FEC865769571B4153C5D96FB3C65741CB2F41B969571B4137D2A3BCB0C65741C0C8B2C169571B4160F1C841B0C65741E92D79E369571B41456E1FEAAFC6574100C7EAEB69571B410D576634AFC6574115000000A2E470A670571B411AAE55EE83C65741A2E470A670571B41D24B730384C657412D8C225D6F571B415E4C33A68CC657411B4A7BAE83571B416758D4648EC65741B937A8A580571B41294CAD8D90C65741694E8C9A6B571B41384441BE91C657410D888F026B571B41C77BDBB9A8C65741093A732A85571B41B504D320AAC657412937A5F67B571B4147A3AD29ACC65741E2E535CD6B571B41034BC575ADC6574100C7EAEB69571B4185775C2DAFC65741FCC422166A571B41C209D4E3ADC65741CB4563365E571B4135DE0800ADC65741AA445DD854571B41AEC57ED6ABC65741EACDC90B5B571B41A4B9DD17AAC65741B67A147B67571B41F798258BA8C65741B67A147B67571B417A11144B91C65741EACDC90B5B571B419CE82D5B8FC65741D657363F61571B41877D8A058DC657411C8C225D6F571B412049877E8CC65741A2E470A670571B411AAE55EE83C6574104000000216DE37B81571B41900D5A1317C65741BF93869A81571B41A659491517C6574149FEA43D81571B410B39E18319C65741216DE37B81571B41900D5A1317C657410D0000009C56F38682571B4168B4973511C657419C56F38682571B416951DF3611C657419C56F38682571B416CEE263811C657419A24545C95571B41A06C4E5411C657410E3BFBF6AA571B41571357C013C65741828CE8B19E571B4144EAEAB715C657418F954E7081571B413161B04416C6574156FCDC6781571B411A51161217C657418F6BE37B81571B41770D5A1317C6574144CD04E55F571B41A1BFEBA715C657411865708F56571B419AF18F5313C657413002B62969571B410DB4929511C657419C56F38682571B4168B4973511C6574107000000B5B7C601BC4E1B4199252600BBC657417B98C5C1DB4E1B41B46359CFB9C657418B636EAC104F1B41B46359CFB9C65741FD23A15F1D4F1B41A9C4D052BCC657410620CFF70F4F1B41263748DABCC657416F867687CB4E1B41911FDCF0BCC65741B5B7C601BC4E1B4199252600BBC65741";

        // END NOTE

        private const string ExpectedResult = @"GEOMETRYCOLLECTION (LINESTRING (447968.63178764074 6232132.837384242, 447968.631787637 6232132.837384321), LINESTRING (447968.3709847265 6232156.302371374, 447968.3709847499 6232156.302371398), LINESTRING (447962.480387792 6232764.81874634, 447962.474320096 6232767.11412815), POLYGON ((447968.701300134 6232125.715481, 447800.87791023 6232122.0212554, 447667.52798224 6232118.3170907, 447667.52798224 6232163.8253994, 447666.690152371 6232241.95127252, 447666.70195289 6232269.29960783, 447665.32016632 6232369.69959036, 447663.82381757 6232509.2309641, 447661.310889315 6232661.0118306, 447659.00088514 6232828.85568136, 447880.43723736 6232832.74052964, 447960.181600783 6232833.47957667, 447962.404854601 6232773.89637429, 447962.43091273 6232770.94749885, 447962.43915857 6232769.02788958, 447962.47214195 6232767.65816838, 447962.480387792 6232764.81874634, 447962.480387792 6232764.75875855, 447962.480387792 6232764.70876873, 447962.52161701 6232759.55981678, 447959.55311307 6232756.00054126, 447957.21129329 6232751.35148756, 447958.76151201 6232744.37290803, 447961.87019531 6232738.17416977, 447961.870195314 6232732.28578422, 447961.74449003 6232728.1148205, 447961.74449003 6232725.998155, 447961.870195314 6232714.50898591, 447961.87019531 6232645.17309987, 447958.76151201 6232637.42467704, 447960.31173074 6232628.08657778, 447962.832244316 6232626.57995612, 447963.87887844 6232530.92004396, 447961.52386985 6232528.79678793, 447961.52386985 6232524.9275755, 447963.998814869 6232519.95813469, 447966.049959616 6232332.48829994, 447961.52386985 6232327.30780043, 447966.179482514 6232320.65020981, 447966.941389556 6232251.01368731, 447966.94138956 6232234.30673053, 447963.8409521 6232227.33814896, 447966.94138956 6232218.80988487, 447967.550605288 6232195.33279361, 447967.56532022 6232193.9878832, 447968.345104083 6232160.45717706, 447968.3709847265 6232156.302371374, 447959.97365113 6232150.62376395, 447957.64007719 6232141.30566063, 447962.29073337 6232134.33707906, 447968.631787637 6232132.837384321, 447968.701300134 6232125.715481), (447793.6459701 6232302.2387078, 447796.11541321 6232293.4192681, 447802.11263219 6232290.949825, 447808.10985118 6232294.4776009, 447810.2265167 6232302.2387078, 447813.40151499 6232309.9998147, 447814.81262534 6232313.5275906, 447818.6931788 6232327.2859165, 447817.63484603 6232335.399801, 447818.34040121 6232338.2220217, 447816.92929086 6232345.2775735, 447810.2265167 6232345.9831286, 447804.93485289 6232344.9247959, 447801.40707702 6232338.9275769, 447800.70152185 6232328.3442493, 447797.87930115 6232318.4664768, 447794.35152527 6232308.2359268, 447793.6459701 6232302.2387078), (447811.637626875 6232500.8524926, 447811.99040447 6232491.3274977, 447811.63762688 6232490.2691649, 447817.28206827 6232483.5663908, 447826.80706313 6232485.68305626, 447829.100117447 6232492.38583046, 447829.27650624 6232500.499715, 447827.86539589 6232514.2580409, 447827.51261831 6232528.0163668, 447817.63484586 6232530.8385875, 447811.284849291 6232518.49137189, 447811.637626875 6232500.8524926), (447840.91816652 6232640.9051964, 447844.09316481 6232637.3774205, 447850.79593896 6232637.3774205, 447857.49871312 6232643.0218619, 447865.61259763 6232653.6051895, 447868.08204074 6232666.6579602, 447866.3181528 6232676.1829551, 447857.85149071 6232678.6523982, 447852.5598269 6232673.713512, 447849.03205103 6232658.1912981, 447842.32927687 6232647.9607481, 447840.91816652 6232640.9051964)))";

        private const string NonNodedIntersection = @"MULTILINESTRING ((447968.351428931 6232156.28261211, 447968.37098472641 6232156.3023713743),(447968.37098472653 6232156.3023713743, 447959.97365113 6232150.62376395))";
        private const double BufferValue = 31.875;

        [Test]
        public void test_intersection_bug()
        {
            Assert.DoesNotThrow(() => DoIntersection(FromWkb(Poly1Wkb), FromWkb(Poly2Wkb)));
            Assert.DoesNotThrow(() => DoIntersection(FromWkt(Poly1Wkt), FromWkt(Poly2Wkt)));
        }

        [Test]
        public void test_intersection_bug_clipped()
        {
            IGeometryFactory factory = GeometryFactory.Default;
            WKTReader reader = new WKTReader(factory);
            IGeometry geom = reader.Read(NonNodedIntersection);
            Envelope clipEnv = geom.EnvelopeInternal;
            clipEnv.ExpandBy(BufferValue);
            IGeometry clip = factory.ToGeometry(clipEnv);
            Assert.DoesNotThrow(() => DoIntersection(FromWkb(Poly1Wkb, clip), FromWkb(Poly2Wkb, clip), clip));
        }

        private static IGeometry FromWkt(string wkt)
        {
            return new WKTReader(GeometryFactory.Default).Read(wkt);
        }

        private static IGeometry FromWkb(string wkb, IGeometry clip = null)
        {
            WKBReader reader = new WKBReader(GeometryFactory.Default);
            byte[] bytes = WKBReader.HexToBytes(wkb);
            IGeometry geom =  reader.Read(bytes);
            if (clip != null)
                geom = geom.Intersection(clip);
            return geom;
        }

        private static void DoIntersection(IGeometry poly1, IGeometry poly2, IGeometry clip = null)
        {
            Assert.IsTrue(poly1.IsValid);
            Assert.IsTrue(poly1 is IPolygon);

            Assert.IsTrue(poly2.IsValid);
            Assert.IsTrue(poly2 is IPolygon);

            IGeometry intersection = poly1.Intersection(poly2);
            Assert.IsNotNull(intersection);
            Assert.IsTrue(intersection.IsValid);

            WKTReader reader = new WKTReader();
            IGeometry expectedIntersection = reader.Read(ExpectedResult);
            if (clip != null)
                expectedIntersection = expectedIntersection.Intersection(clip);

            double hd = DiscreteHausdorffDistance.Distance(intersection, expectedIntersection);
            Assert.That(hd < 0.001, "Intersection error: result not same as JTS");
        }
    }
}