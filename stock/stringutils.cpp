#include "stdafx.h"
#include "stringutils.h"
#include <QUuid>
#include <QCryptographicHash>
#include <QTime>


QString StringUtils::md5(const QString &text)
{
	QCryptographicHash hash(QCryptographicHash::Md5);
	hash.addData(text.toUtf8());
	QByteArray result = hash.result();
	return result.toHex();
}

QStringList StringUtils::splitString(const QString &str, const QString &spit)
{
	QStringList list = str.split(spit);
	for (int i = 0; i < list.length(); i++)
	{
		QString v = list[i];
		if (v.length() < 2) continue;
		if (v[0] == '\"' && v[v.length() - 1] == '\"')
		{
			list[i] = v.mid(1, v.length() - 2);
		}
	}
	return list;
}

static char* cLowChineseChars[] =
{
	"啊","芭","擦","搭","蛾","发","噶","哈","击","喀",
	"垃","妈","拿","欧","啪","期","然","撒","塌","挖",
	"昔","压","匝"
};
static char cLowPinyins[] =
{
	'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k',
	'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'w',
	'x', 'y', 'z'
};

static char* cHighPinyins[] =
{
	/*216*/"cjwgnspgcgnesypbtyyzdxykygtdjnmjqmbsgzscyjsyyzpgkbzgycywykgkljswkpjqhyzwddzlsgmrypywwcckznkydg",
	/*217*/"ttnjjeykkzytcjnmcylqlypyqfqrpzslwbtgkjfyxjwzltbncxjjjjzxdttsqzycdxxhgckbphffssyybgmxlpbylllhlx",
	/*218*/"spzmyjhsojnghdzqyklgjhxgqzhxqgkezzwyscscjxyeyxadzpmdssmzjzqjyzcdjewqjbdzbxgznzcpwhkxhqkmwfbpby",
	/*219*/"dtjzzkqhylygxfptyjyyzpszlfchmqshgmxxsxjjsdcsbbqbefsjyhxwgzkpylqbgldlcctnmayddkssngycsgxlyzaybn",
	/*220*/"ptsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrxccqwqqsbzkymgplbmjrqcflnymyqmsqyrbcjthztqfrxqhxmjjcjlx",
	/*221*/"qgjmshzkbswyemyltxfsydsglycjqxsjnqbsctyhbftdcyzdjwyghqfrxwckqkxebptlpxjzsrmebwhjlbjslyysmdxlcl",
	/*222*/"qkxlhxjrzjmfqhxhwywsbhtrxxglhqhfnmcykldyxzpwlggsmtcfpajjzyljtyanjgbjplqgdzyqyaxbkysecjsznslyzh",
	/*223*/"zxlzcghpxzhznytdsbcjkdlzayfmydlebbgqyzkxgldndnyskjshdlyxbcghxypkdqmmzngmmclgwzszxzjfznmlzzthcs",
	/*224*/"ydbdllscddnlkjykjsycjlkohqasdknhcsganhdaashtcplcpqybsdmpjlpcjoqlcdhjjysprchnknnlhlyyqyhwzptczg",
	/*225*/"wwmzffjqqqqyxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmssjzldbydcpcxyhlxchyzjqsqqagmnyxpfrkssbjlyxy",
	/*226*/"syglnscmhcwwmnzjjlxxhchsyd ctxrycyxbyhcsmxjsznpwgpxxtaybgajcxlysdccwzocwkccsbnhcpdyznfcyytyckx",
	/*227*/"kybsqkkytqqxfcwchcykelzqbsqyjqcclmthsywhmktlkjlycxwheqqhtqhzpqsqscfymmdmgbwhwlgsllysdlmlxpthmj",
	/*228*/"hwljzyhzjxhtxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsxymjshxpjxwmyqksmyplrthbxftpmhyxlchlhlzylxgsssstcl",
	/*229*/"sldclrpbhzhxyyfhbbgdmycnqqwlqhjjzywjzyejjdhpblqxtqkwhlchqxagtlxljxmslxhtzkzjecxjcjnmfbycsfywyb",
	/*230*/"jzgnysdzsqyrsljpclpwxsdwejbjcbcnaytwgmpabclyqpclzxsbnmsggfnzjjbzsfzyndxhplqkzczwalsbccjxjyzhwk",
	/*231*/"ypsgxfzfcdkhjgxdlqfsgdslqwzkxtmhsbgzmjzrglyjbpmlmsxlzjqqhzsjczydjwbmjklddpmjegxyhylxhlqyqhkycw",
	/*232*/"cjmyyxnatjhyccxzpcqlbzwwytwbqcmlpmyrjcccxfpznzzljplxxyztzlgdldcklyrlzgqtgjhhgjljaxfgfjzslcfdqz",
	/*233*/"lclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqctzhgyqtjqqlzxjylylbcyamcstylpdjbyregkjzyzhlyszqlznw",
	/*234*/"czcllwjqjjjkdgjzolbbzppglghtgzxyghzmycnqsycyhbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkqsb",
	/*235*/"gbmmcjssclpqpdxcdyykywcjddyygywrhjrtgznyqldkljszzgzqzjgdykshpzmtlcpwnjafyzdjcnmwescyglbtzcgmss",
	/*236*/"llyxqsxsbsjsbbggghfjlypmzjnlyywdqshzxtyywhmcyhywdbxbtlmsyyyfsxjcsdxxlhjhf sxzqhfzmzcztqcxzxrtt",
	/*237*/"djhnnyzqqmnqdmmglydxmjgdhcdyzbffallztdltfxmxqzdngwqdbdczjdxbzgsqqddjcmbkzffxmkdmdsyyszcmljdsyn",
	/*238*/"sprskmkmpcklgdbqtfzswtfgglyplljzhgjjgypzltcsmcnbtjbqfkthbyzgkpbbymtdssxtbnpdkleycjnycdykzddhqh",
	/*239*/"sdzsctarlltkzlgecllkjlqjaqnbdkkghpjtzqksecshalqfmmgjnlyjbbtmlyzxdcjpldlpcqdhzycbzsczbzmsljflkr",
	/*240*/"zjsnfrgjhxpdhyjybzgdljcsezgxlblhyxtwmabchecmwyjyzlljjyhlgbdjlslygkdzpzxjyyzlwcxszfgwyydlyhcljs",
	/*241*/"cmbjhblyzlycblydpdqysxqzbytdkyyjyycnrjmpdjgklcljbctbjddbblblczqrppxjcglzcshltoljnmdddlngkaqhqh",
	/*242*/"jhykheznmshrp qqjchgmfprxhjgdychghlyrzqlcyqjnzsqtkqjymszswlcfqqqxyfggyptqwlmcrnfkkfsyylqbmqamm",
	/*243*/"myxctpshcptxxzzsmphpshmclmldqfyqxszyjdjjzzhqpdszglstjbckbxyqzjsgpsxqzqzrqtbdkyxzkhhgflbcsmdldg",
	/*244*/"dzdblzyycxnncsybzbfglzzxswmsccmqnjqsbdqsjtxxmbltxzclzshzcxrqjgjylxzfjphyxzqqydfqjjlzznzjcdgzyg",
	/*245*/"ctxmzysctlkphtxhtlbjxjlxscdqxcbbtjfqzfsltjbtkqbxxjjljchczdbzjdczjdcprnpqcjpfczlclzxbdmxmphjsgz",
	/*246*/"gszzqlylwtjpfsyasmcjbtzyycwmytcsjjlqcqlwzmalbxyfbpnlsfhtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbs",
	/*247*/"aqtgylbxmmygszldydqmjjrgbjtkgdhgkblqkbdmbylxwcxyttybkmrtjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz"
};


static int cPinyinMapHz[] = 
{
	19969,19975,19988,20048,20056,20060,20094,20127,20167,20193,
		20250,20256,20282,20285,20291,20314,20340,20375,20389,20391,
		20415,20446,20447,20504,20608,20854,20857,20911,20504,20608,
		20854,20857,20985,21032,21048,21049,21089,21119,21242,
		21273,21305,21306,21330,21333,21345,21378,21397,21414,21442,
		21477,21480,21484,21494,21505,21512,21523,21537,21542,21549,
		21574,21588,21618,21621,21632,21654,21679,21683,
		21719,21734,21780,21804,21899,21903,21908,
		21939,21956,21964,21970,22031,22040,22060,22066,22079,
		22129,22179,22237,22244,22280,22300,22313,22331,22343,22351,
		22395,22412,22484,22500,22534,22549,22561,22612,22771,22831,
		22841,22855,22865,23013,23081,23487,23558,23561,23586,23614,
		23615,23631,23646,23663,23673,23762,23769,23780,23884,24055,
		24113,24162,24191,24273,24324,24377,24378,24439,24554,24683,
		24694,24733,24925,25094,25100,25103,25153,25170,25179,25203,
		25240,25282,25303,25324,25341,25373,25375,25528,
		25530,25552,25774,25874,26044,26080,26292,26333,26355,26366,
		26397,26399,26415,26451,26526,26552,26561,26588,26597,26629,
		26638,26646,26653,26657,26727,26894,26937,26946,26999,27099,
		27449,27481,27542,27663,27748,27784,27788,27795,27850,
		27852,27895,27898,27973,27981,27986,27994,28044,28065,28177,
		28267,28291,28337,28463,28548,28601,28689,28805,28820,28846,
		28952,28975,29325,29575,29602,30010,30044,30058,30091,
		30111,30229,30427,30465,30631,30655,30684,30707,30729,30796,
		30917,31074,31085,31109,31181,31192,31293,31400,31584,31896,
		31909,31995,32321,32327,32418,32420,32421,32438,32473,32488,
		32521,32527,32562,32564,32735,32793,33071,33098,33100,33152,
		33261,33324,33333,33406,33426,33432,33445,33486,33493,33507,
		33540,33544,33564,33617,33632,33636,33637,33694,33705,33728,
		33882,34067,34074,34121,34255,34259,34425,34430,34485,34503,
		34532,34552,34558,34593,34660,34892,34928,34999,35048,35059,
		35098,35203,35265,35299,35782,35828,35843,35895,35977,
		36158,36228,36426,36466,36710,36711,36767,36866,36951,37034,
		37063,37218,37325,38063,38079,38085,38107,38116,38123,38224,
		38241,38271,38415,38426,38461,38463,38466,38477,38518,38551,
		38585,38704,38739,38761,38808,39048,39049,39052,39076,39271,
		39534,39552,39584,39647,39730,39748,40109,40479,40516,40536,
		40583,40765,40784,40840,40863};

static char* cPinyinMapPy[] = 
{
		"dz","wm","qj","ly","cs","mn","qg","qj","cq","yg",
			"hk","cz","cs","jgq","dt","yd","ne","dt","jy","cz",
			"bp","ys","sq","tc","kg","qj","zc","fp","tc","kg",
			"qj","zc","aw","pb","qx","cs","ys","jc","sb",
			"sc","py","qo","zc","dcs","kq","ca","cs","sx","cs",
			"jg","dt","zs","yx","yx","hg","xh","pb","fp","kh",
			"da","td","zc","kha","jz","gk","lkg","kh",
			"hy","woe","wn","hx","dz","nr","wo",
			"zc","sa","ya","td","jg","xs","zc","cz","hm",
			"xj","xa","nj","td","qj","yh","xw","yq","jy","hp",
			"dc","td","pb","pb","dz","dh","bp","td","kq","hb",
			"jg","qj","qx","lm","mw","sx","jq","wy","yw","wy",
			"ns","pb","sz","tz","yg","dt","zs","qj","qk","xh",
			"dc","cz","ga","qj","nl","td","qj","pf","zs","td",
			"ew","lk","tn","zg","xq","xh","bp","bp","kg","bp",
			"zs","fb","na","kg","zy","wz","xj","sd",
			"cs","td","cz","zc","yw","wm","bp","pb","zy","cz",
			"cz","qj","sz","sb","cz","jg","td","gj","cz","zs",
			"ly","qx","kg","xj","gh","zc","zs","zc","kj","kj",
			"yq","xs","zs","zs","ts","sc","zd","td","bp",
			"mb","ls","lp","qj","kh","hx","xj","yc","wg","sm",
			"qj","kh","zq","tl","dc","td","pb","jg","qg","pb",
			"td","zc","qj","sl","fb","td","cx","pf","ysp",
			"yn","xj","sc","sx","qy","qj","qjg","sd","xh","lg",
			"bp","mn","jz","cs","zc","mlb","jq","yx","jy","zn",
			"zy","xj","fp","yz","hg","xq","hg","lg","gj","dt",
			"jq","pb","zsq","jz","zd","pb","pf","lx","ya","bp",
			"cx","bp","dt","ay","mw","pb","jg","zn","st","jq",
			"qj","zc","qx","yt","jq","hx","yx","gw","fp","wy",
			"sr","mw","wy","jq","cz","xl","hj","xh","kh","sy",
			"gh","sx","ey","lz","qy","hx","sc","jq","bp","sc",
			"cz","tq","jx","jx","sz","sy","td","gy","hm",
			"jg","qj","qx","dc","cj","zyg","bp","sk","yw","xy",
			"hx","cz","zc","bp","dt","qy","dc","td","yd","hg",
			"xtc","cz","ye","kh","yd","ae","pb","jx","ty","wk",
			"zc","xs","lj","gj","qs","jg","jx","hg","cz","xt",
			"td","td","bp","sb","lg","pbt","zq","nd","hg","hg",
			"qj","yq","qj","yk","gjq"
};

static QString cSymbolChineseStart = QString::fromLocal8Bit("！");
static QString cSymbolChineseEnd = QString::fromLocal8Bit("～");
static int cSymbolChineseStartCode = cSymbolChineseStart.at(0).unicode();
static int cSymbolChineseEndCode = cSymbolChineseEnd.at(0).unicode();
const int cSymbolChineseOffset = cSymbolChineseStartCode - (int)'!';

QHash<int, char*> *pinyinMap = NULL;

int compareChineseChar(QByteArray &bytes1, char* char2)
{
	QString str2(char2);	
	QByteArray bytes2 = str2.toLocal8Bit();

	uchar byte10 = uchar(bytes1[0]);
	uchar byte20 = uchar(bytes2[0]);
	if (byte10 > byte20)    // 先比第一个字节
	{
		return 1;
	}
	else if (byte10 == byte20)   // 如果第一个字节相等，则比第二个
	{
		return uchar(bytes1[1]) - uchar(bytes2[1]);
	}
	else
	{
		return -1;
	}
}

static QChar getHighPinyins(QByteArray bytes)
{
	return cHighPinyins[uchar(bytes[0]) - 216][uchar(bytes[1]) - 160 - 1];
}

QChar getSingleCharPinyinCode(QChar chineseChar)
{
	QString str(chineseChar);	
	QByteArray bytes = str.toLocal8Bit();

	if (uchar(bytes[0]) < uchar(216))
	{
		for (int i = sizeof(cLowChineseChars) / sizeof(char*) - 1; i >= 0; i--)
		{
			char* char2 = cLowChineseChars[i];
			if (compareChineseChar(bytes, char2) >= 0)
			{
				return cLowPinyins[i];
			}
		}
		return 0; // 全角符号，如 （）
	}
	else
	{
		return getHighPinyins(bytes);
	}
}

QChar getCharPinyinCode(QChar chineseChar)
{
	if (!pinyinMap)
	{
		pinyinMap = new QHash<int, char*>();
		for (int i = 0; i < sizeof(cPinyinMapHz) / sizeof(int); i++)
		{
			pinyinMap->insert(cPinyinMapHz[i], cPinyinMapPy[i]);
		}
	}
	char* pyCodes = (char*)pinyinMap->value(chineseChar.unicode(), NULL);
	if (pyCodes)
	{
		return pyCodes[0];
	}

	return getSingleCharPinyinCode(chineseChar);
}

QString StringUtils::getPinyinCode(const QString &str, int maxLength)
{
	QString result;	
	if (str.isEmpty())
	{
		return result;
	}	
	for (int i = 0; i < str.length(); i ++)
	{
		QChar ch = str.at(i);
		QChar newCh = ch;
		QChar pyCode;

		if (ch >= cSymbolChineseStartCode && ch <= cSymbolChineseEndCode) // 65281 到 65374
		{
			newCh = QChar(ch.unicode() - cSymbolChineseOffset);
		}
		else if (ch > 127)  // 表示是中文
		{
			pyCode = getCharPinyinCode(ch);
		}

		if (pyCode == 0 &&
			((newCh >= '0' && newCh <= '9') ||
			(newCh >= 'A' && newCh <= 'Z') || 
			(newCh >= 'a' && newCh <= 'z'))) // 只要汉字、字母、数字)
		{
			pyCode = newCh.toLower();
		}
		if (pyCode > 0)
		{
			result.append(pyCode);
			if (maxLength > 0&& result.length() >= maxLength)
			{
				break;
			}
		}
	}
	return result;
}

QString StringUtils::getPinyinCode(const QString &str)
{
	return getPinyinCode(str, 0);
}

QString StringUtils::newGuid()
{
	QString result = QUuid::createUuid().toString();
	return result.mid(1, result.length() - 2); // 去掉前后的{}
}

static int defaultStartKey = 12, defaultMultKey = 01, defaultAddKey = 31;

// 简单加密，Delphi中有对应的解密函数
QString StringUtils::simpleEncrypt(const QString &str)
{
    QTime time = QTime::currentTime();
	qsrand(time.msec() + time.second() * 1000);
	int randomKey = qrand() % 100;
    int startKey = defaultStartKey + randomKey;
    int multKey = defaultMultKey + randomKey;
    int addKey = defaultAddKey + randomKey;
    QByteArray answer;
	answer.append((uchar)randomKey);
	QByteArray bytes = str.toLocal8Bit();
    for (int i = 0; i < bytes.count(); i ++)
    {
		uchar b = bytes.at(i);
		uchar xorByte = (uchar)(b ^ (startKey >> 8));
		answer.append(xorByte);
        startKey = (xorByte + startKey) * multKey + addKey;
    }
	return answer.toHex();
}

// 简单解密，Delphi中有对应的加密函数
QString StringUtils::simpleDecrypt(const QString &str)
{
	QByteArray bytes = QByteArray::fromHex(str.toLocal8Bit());
	if (bytes.count() <= 1) return "";
	int randomKey = (int)bytes.at(0);
    int startKey = defaultStartKey + randomKey;
    int multKey = defaultMultKey + randomKey;
    int addKey = defaultAddKey + randomKey;

    QByteArray answer;
	for (int i = 1; i < bytes.count(); i ++)
    {
		uchar b = bytes.at(i);
		uchar xorByte = (uchar)(b ^ (startKey >> 8));
		answer.append(xorByte);
        startKey = (b + startKey) * multKey + addKey;
    }
	return QString::fromLocal8Bit(answer.data(), answer.size());
}

