import { AARSTemplatePage } from './app.po';

describe('AARS App', function () {
    let page: AARSTemplatePage;

    beforeEach(() => {
        page = new AARSTemplatePage();
    });

    it('should display message saying app works', () => {
        page.navigateTo();
        expect(page.getParagraphText()).toEqual('app works!');
    });
});
