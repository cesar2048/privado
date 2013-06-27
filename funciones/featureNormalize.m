function [X_norm, mu, sigma] = featureNormalize(X)
%normalizando las funciones para que esten en el mismo rango y no afecte el analisis
X_norm = X;
mu = zeros(1, size(X, 2));
sigma = zeros(1, size(X, 2));
      
mu=mean(X_norm);
sigma= std(X_norm);
tam=size(X_norm,1);
vMu=(mu'*ones(1,tam))';
vDiv=(sigma'*ones(1,tam))';
xRest=X_norm-vMu;
X_norm=xRest./vDiv;


end
